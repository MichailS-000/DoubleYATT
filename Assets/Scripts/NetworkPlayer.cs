using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Transform headTransform;
    [SerializeField] Transform leftHeadTransform;
    [SerializeField] Transform rightHeadTransform;

    [SerializeField] PhotonView view;

    [SerializeField] GameObject[] objectsToDelete;
	[SerializeField] InputActionManager actionManager;
	[SerializeField] Rigidbody rb;
    [SerializeField] PhysicsOrigin origin;

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
        if (stream.IsWriting)
		{
            stream.SendNext(headTransform);
            stream.SendNext(leftHeadTransform);
            stream.SendNext(rightHeadTransform);
		}
		else
		{
            headTransform = (Transform)stream.ReceiveNext();
            leftHeadTransform = (Transform)stream.ReceiveNext();
            rightHeadTransform = (Transform)stream.ReceiveNext();
		}
	}

	void Start()
    {
        if (!view.IsMine)
		{
            Destroy(origin);
            Destroy(rb);
            Destroy(headTransform.gameObject.GetComponent<Camera>());

            foreach(GameObject go in objectsToDelete)
			{
                Destroy(go);
            }

            Destroy(actionManager);
        }
    }
}