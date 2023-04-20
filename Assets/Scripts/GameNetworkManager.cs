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

		player.position = spawn.position;
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
