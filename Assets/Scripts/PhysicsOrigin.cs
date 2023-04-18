using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsOrigin : MonoBehaviour
{
    [SerializeField] Transform playerHead, leftController, rightController;
	[SerializeField] ConfigurableJoint headJoint, leftControllerJoint, rightControllerJoint;
    [SerializeField] CapsuleCollider bodyCollider;

    [SerializeField] float bodyHeightMin = 0.5f;
    [SerializeField] float bodyHeightMax = 2f;

    void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);

		leftControllerJoint.targetPosition = leftController.localPosition;
		leftControllerJoint.targetRotation = leftController.localRotation;

		rightControllerJoint.targetPosition = rightController.localPosition;
		rightControllerJoint.targetRotation = rightController.localRotation;

		headJoint.targetPosition = playerHead.localPosition;
	}
}