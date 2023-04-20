using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;
using System.Collections.Generic;

struct LerpTransform
{
    public Vector3 pos;
    public Quaternion rot;
}

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Transform headTransform;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;

    [SerializeField] PhotonView view;

    [SerializeField] float smooth = 10f;

    [SerializeField] GameObject[] objectsToDelete;
	[SerializeField] InputActionManager actionManager;
    [SerializeField] ActionBasedController[] controllers;
	[SerializeField] Rigidbody rb;
    [SerializeField] PhysicsOrigin origin;
    [SerializeField] XROrigin XR_Origin;
    [SerializeField] TrackedPoseDriver tracketDriver;        
    [SerializeField] Camera cam;
    [SerializeField] MeshRenderer headRenderer;

    Dictionary<Transform, LerpTransform> lerpTransforms = new Dictionary<Transform, LerpTransform>();

    void SendTransform(PhotonStream stream, Transform transf)
	{
        Vector3 position = transf.position;
        stream.SendNext(position.x);
        stream.SendNext(position.y);
        stream.SendNext(position.z);
        Quaternion quaternion = transf.rotation;
        stream.SendNext(quaternion.x);
        stream.SendNext(quaternion.y);
        stream.SendNext(quaternion.z);
        stream.SendNext(quaternion.w);
    }

    void ApplyTransform(PhotonStream stream, Transform transf)
	{
        LerpTransform lerpTransform = new LerpTransform();

        float[] values = new float[4];
        values[0] = (float)stream.ReceiveNext();
        values[1] = (float)stream.ReceiveNext();
		values[2] = (float)stream.ReceiveNext();
		lerpTransform.pos = new Vector3(values[0], values[1], values[2]);

		values[0] = (float)stream.ReceiveNext();
		values[1] = (float)stream.ReceiveNext();
		values[2] = (float)stream.ReceiveNext();
		values[3] = (float)stream.ReceiveNext();
        lerpTransform.rot = new Quaternion(values[0], values[1], values[2], values[3]);

        lerpTransforms[transf] = lerpTransform;
    }

	private void Update()
	{
		if (!view.IsMine)
		{
            headTransform.position = Vector3.Lerp(headTransform.position, lerpTransforms[headTransform].pos, smooth * Time.deltaTime);
            headTransform.rotation = Quaternion.Lerp(headTransform.rotation, lerpTransforms[headTransform].rot, smooth * Time.deltaTime);

            leftHandTransform.position = Vector3.Lerp(leftHandTransform.position, lerpTransforms[leftHandTransform].pos, smooth * Time.deltaTime);
            leftHandTransform.rotation = Quaternion.Lerp(leftHandTransform.rotation, lerpTransforms[leftHandTransform].rot, smooth * Time.deltaTime);

            rightHandTransform.position = Vector3.Lerp(rightHandTransform.position, lerpTransforms[rightHandTransform].pos, smooth * Time.deltaTime);
            rightHandTransform.rotation = Quaternion.Lerp(rightHandTransform.rotation, lerpTransforms[rightHandTransform].rot, smooth * Time.deltaTime);
        }
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        if (stream.IsWriting)
		{
            SendTransform(stream, headTransform);
            SendTransform(stream, leftHandTransform);
            SendTransform(stream, rightHandTransform);
		}
		else
		{
            ApplyTransform(stream, headTransform);
            ApplyTransform(stream, leftHandTransform);
            ApplyTransform(stream, rightHandTransform);
		}
	}

	void Awake()
    {
        lerpTransforms.Add(headTransform, new LerpTransform() { pos = headTransform.position, rot = headTransform.rotation });
        lerpTransforms.Add(leftHandTransform, new LerpTransform() { pos = leftHandTransform.position, rot = leftHandTransform.rotation });
        lerpTransforms.Add(rightHandTransform, new LerpTransform() { pos = rightHandTransform.position, rot = rightHandTransform.rotation });

        if (!view.IsMine)
		{
            Destroy(origin);
            Destroy(rb);
            Destroy(XR_Origin);
            Destroy(tracketDriver);
            cam.gameObject.SetActive(false);

            foreach(GameObject go in objectsToDelete)
			{
                Destroy(go);
            }

            foreach(ActionBasedController controller in controllers)
			{
                Destroy(controller);
			}

            Destroy(actionManager);
        }
		else
		{
            headRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
}