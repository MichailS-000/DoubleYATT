using UnityEngine;
using Photon.Pun;

public class FakeBrick : MonoBehaviourPun
{
	[SerializeField] float breakTime = 3f;
	[SerializeField] Rigidbody rb;

	float time = 0;
	bool breacked = false;

	private void OnCollisionStay(Collision collision)
	{
		time += Time.deltaTime;
		if (time >= breakTime && !breacked)
		{
			breacked = true;

			PhotonView.Get(this).RPC(nameof(OnBreak), RpcTarget.All);
		}
	}

	[PunRPC]
	private void OnBreak()
	{
		rb.isKinematic = false;
	}
}