using Photon.Pun;
using UnityEngine;

public class GameNetworkManager : MonoBehaviourPunCallbacks
{
	public static GameNetworkManager instance;

	float currentTime = 0;

	public float GetTime() { return currentTime; }

	private void Awake()
	{
		instance = this;
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
