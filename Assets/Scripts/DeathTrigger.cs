using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
	[SerializeField] Transform spawnPoint;

	private void OnTriggerEnter(Collider other)
	{
		other.transform.parent.position = spawnPoint.position;
		other.attachedRigidbody.velocity = Vector3.zero;
		other.transform.localPosition = Vector3.zero;
	}
}
