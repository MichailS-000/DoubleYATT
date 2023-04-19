using Photon.Pun;
using UnityEngine;

[System.Serializable]
[SerializeField]
public class GeneratorSegment
{
	public GameObject segmentObject;
	public Vector3 postGenerationAnchor;
	public Vector3 preGenerationAnchor;

	public int[] possibleSegments;
	public int[] possibleRotationOffsets;
}

public class Generator : MonoBehaviour
{
	[SerializeField] Transform[] playerSpawnPositions;
	[SerializeField] GameObject player;

	[SerializeField] GeneratorSegment[] segments;

	GeneratorSegment lastGenerated;
	Vector3 lastGeneratedPosition = Vector3.zero;
	int lastGeneratedRotation = 0;

	int seed;

	void CreateSegment(int segmentId)
	{
		lastGeneratedPosition = lastGeneratedPosition + lastGenerated.postGenerationAnchor + segments[segmentId].preGenerationAnchor;

		Instantiate(segments[segmentId].segmentObject,
			lastGeneratedPosition,
			Quaternion.Euler(0,
			0, 0));

		lastGenerated = segments[segmentId];
	}

	private void Start()
	{
		seed = (int)PhotonNetwork.CurrentRoom.CustomProperties["seed"];
		Debug.Log("Current seed: " + seed);

		Transform random = playerSpawnPositions[Random.Range(0, playerSpawnPositions.Length)];

		Random.InitState(seed);

		PhotonNetwork.Instantiate(player.name, random.position, random.rotation);

		lastGenerated = segments[0];

		CreateSegment(1);

		for (int i = 0; i < 20; i++)
		{
			CreateSegment(lastGenerated.possibleSegments[Random.Range(0, lastGenerated.possibleSegments.Length)]);
		}
	}
}