using Photon.Pun;
using UnityEngine;

public class GameNetworkManager : MonoBehaviourPunCallbacks
{
	public static GameNetworkManager instance;

	[SerializeField] Transform spawn;
	public Transform player; 

	float currentTime = 0;

	float bestRecord = float.MaxValue;

	public float GetTime() { return currentTime; }
	public float GetBestTime() { return bestRecord; }

	private void Awake()
	{
		instance = this;
	}

	public void Finish()
	{
		if (currentTime < bestRecord)
		{
			bestRecord = currentTime;
		}

		currentTime = 0;
		player.localPosition = Vector3.zero;
		player.parent.position = spawn.position;
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void Update()
	{
		currentTime += Time.deltaTime;
	}

	public override void OnLeftRoom()
	{
		PhotonNetwork.LoadLevel(0);
	}
}
