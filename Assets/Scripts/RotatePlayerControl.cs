using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayerControl : MonoBehaviour
{
	private InputAction rotatePlayerAction;
	[SerializeField] private Transform cameraToMove;
	[SerializeField] private GameObject playerToMove;
	[SerializeField] private float speedX;
	[SerializeField] private float speedY;
	[SerializeField] private float xClamp;
	float xRotate = 0f;

	public void Initialize(InputAction rotatePlayerAction)
	{
		this.rotatePlayerAction = rotatePlayerAction;
		rotatePlayerAction.Enable();
	}

	private void FixedUpdate()
	{
		//grab the mouse x and y position into a 2d vector
		Vector2 direction = rotatePlayerAction.ReadValue<Vector2>();

		playerToMove.transform.Rotate(Vector3.up, direction.x * speedX * Time.deltaTime);

		xRotate -= direction.y * speedY;
		xRotate = Mathf.Clamp(xRotate, -xClamp, xClamp);
		Vector3 targetRotate = playerToMove.transform.eulerAngles;
		targetRotate.x = xRotate;
		cameraToMove.transform.eulerAngles = targetRotate;

		cameraToMove.transform.position = playerToMove.transform.position;
	}
}
