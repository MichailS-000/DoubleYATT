using UnityEngine;

public class Flag : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		GameNetworkManager.instance.Finish();
	}
}
