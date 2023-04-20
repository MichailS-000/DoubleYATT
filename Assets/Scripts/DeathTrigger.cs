using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;

	private void OnCollisionEnter(Collision collision)
	{
		collision.collider.attachedRigidbody.velocity = Vector3.zero;
		collision.rigidbody.position = spawnPoint.position;
	}
}
