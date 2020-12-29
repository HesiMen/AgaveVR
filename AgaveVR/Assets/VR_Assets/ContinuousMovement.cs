using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
	public float speed = 1.0f;
	public float gravity = -9.81f;
	public float additionalHeight = 0.2f;
	public LayerMask groundLayer;
	public XRNode inputSource;

	private float fallingSpeed = 0.0f;
	private XRRig rig;
	private Vector2 inputAxis;
	private CharacterController character;

	// Start is called before the first frame update
	void Start()
	{
		character = GetComponent<CharacterController>();
		rig = GetComponent<XRRig>();
	}

	// Update is called once per frame
	void Update()
	{
		InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
		device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
	}

	private void FixedUpdate()
	{
		CapsuleFollowHeadset();

		Quaternion headYaw = Quaternion.Euler(0.0f, rig.cameraGameObject.transform.eulerAngles.y, 0.0f);
		Vector3 direction = headYaw * new Vector3(inputAxis.x, 0.0f, inputAxis.y);

		character.Move(direction * Time.fixedDeltaTime * speed);

		// Gravity
		bool isGrounded = CheckIfGrounded();
		if (isGrounded)
		{
			fallingSpeed = 0.0f;
		}
		else
		{
			fallingSpeed += gravity * Time.fixedDeltaTime;
		}
		character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
	}
	
	void CapsuleFollowHeadset()
    {
		character.height = rig.cameraInRigSpaceHeight + additionalHeight;
		Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
		character.center = new Vector3(capsuleCenter.x, character.height / 2.0f + character.skinWidth, capsuleCenter.z);
    }

	bool CheckIfGrounded()
	{
		// tells us if on ground
		Vector3 rayStart = transform.TransformPoint(character.center);
		float rayLength = character.center.y + 0.01f;

		bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
		return hasHit;
	}
}
