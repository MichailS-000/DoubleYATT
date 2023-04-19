using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Transform headTransform;
    [SerializeField] Transform leftHeadTransform;
    [SerializeField] Transform rightHeadTransform;

    [SerializeField] PhotonView view;

    [SerializeField] GameObject[] objectsToDelete;
	[SerializeField] InputActionManager actionManager;
    [SerializeField] ActionBasedController[] controllers;
	[SerializeField] Rigidbody rb;
    [SerializeField] PhysicsOrigin origin;
    [SerializeField] XROrigin XR_Origin;
    [SerializeField] TrackedPoseDriver tracketDriver;        
    [SerializeField] Camera cam;

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
        float[] values = new float[4];
        values[0] = (float)stream.ReceiveNext();
        values[1] = (float)stream.ReceiveNext();
		values[2] = (float)stream.ReceiveNext();
        transf.position = new Vector3(values[0], values[1], values[2]);

		values[0] = (float)stream.ReceiveNext();
		values[1] = (float)stream.ReceiveNext();
		values[2] = (float)stream.ReceiveNext();
		values[3] = (float)stream.ReceiveNext();
        transf.rotation = new Quaternion(values[0], values[1], values[2], values[3]);
    }

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        if (stream.IsWriting)
		{
            SendTransform(stream, headTransform);
            SendTransform(stream, leftHeadTransform);
            SendTransform(stream, rightHeadTransform);
		}
		else
		{
            ApplyTransform(stream, headTransform);
            ApplyTransform(stream, leftHeadTransform);
            ApplyTransform(stream, rightHeadTransform);
		}
	}

	void Awake()
    {
        if (!view.IsMine)
		{
            Destroy(origin);
            Destroy(rb);
            Destroy(XR_Origin);
            Destroy(tracketDriver);
            DestroyImmediate(cam);

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
    }
}