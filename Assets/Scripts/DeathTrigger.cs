using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;

	private void OnTriggerEnter(Collider other)
	{
		other.attachedRigidbody.velocity = Vector3.zero;
		other.attachedRigidbody.position = spawnPoint.position;
	}
}
