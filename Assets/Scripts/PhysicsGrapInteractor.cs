using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsGrapInteractor : MonoBehaviour
{
	[SerializeField] InputActionProperty grapInputSource;
	[SerializeField] float radius;
	[SerializeField] LayerMask grapLayer;

	FixedJoint joint;

	bool isGrabButtonPressed;
	bool isGrabbing = false;

	private void FixedUpdate()
	{
		isGrabButtonPressed = grapInputSource.action.ReadValue<float>() > 0.1f;

		if (isGrabButtonPressed && !isGrabbing)
		{
			Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grapLayer, QueryTriggerInteraction.Ignore);

			if (nearbyColliders.Length > 0)
			{

				Rigidbody rb = nearbyColliders[0].attachedRigidbody;

				joint = gameObject.AddComponent<FixedJoint>();
				joint.autoConfigureConnectedAnchor = false;

				if (rb)
				{
					joint.connectedBody = rb;
					joint.connectedAnchor = rb.transform.InverseTransformPoint(transform.position);
				}
				else
				{
					joint.connectedAnchor = transform.position;
				}

				isGrabbing = true;
			}
		}
		else if (!isGrabButtonPressed && isGrabbing)
		{
			isGrabbing = false;

			if (joint)
			{
				Destroy(joint);
			}
		}
	}
}
