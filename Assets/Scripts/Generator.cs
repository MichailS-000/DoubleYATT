using Photon.Pun;
using UnityEngine;

public class Generator : MonoBehaviour
{
	[SerializeField] Transform[] playerSpawnPositions;
	[SerializeField] GameObject player;

	int seed;

	private void Start()
	{
		seed = (int)PhotonNetwork.CurrentRoom.CustomProperties["seed"];

		Transform random = playerSpawnPositions[Random.Range(0, playerSpawnPositions.Length)];
		PhotonNetwork.Instantiate(player.name, random.position, random.rotation);
	}
}