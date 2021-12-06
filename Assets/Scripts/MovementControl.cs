using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private GameObject playerToMove;
	private float originalSpeed;
	private InputAction moveAction;


	public void Initialize(InputAction moveAction, InputAction speedAction)
	{
		this.moveAction = moveAction;

		//initialize originalSpeed to speed var for reseting later
		originalSpeed = speed;

		//shift button being performed and cancelled 
		speedAction.performed += SpeedEnhanced;
		speedAction.canceled += ResetSpeed;

		moveAction.Enable();
		speedAction.Enable();
	}

	private void SpeedEnhanced(InputAction.CallbackContext obj)
	{
		//double speed if shift key is pressed
		speed = speed * 2;
	}

	private void ResetSpeed(InputAction.CallbackContext obj)
	{
		//if shift key is cancelled reset to normal speed
		speed = originalSpeed;
	}

	private void FixedUpdate()
	{
		//Professor said to use the x and z values and scale the speed
		//Use the game object transform position method to be able to do this
		Vector2 direction = moveAction.ReadValue<Vector2>();

		//move the player using Vector3.MoveTowards in the direction that they are currently facing (forward towards direction they are facing, backwards away from direction they are facing, left and right respectively)
		playerToMove.transform.position = Vector3.MoveTowards(playerToMove.transform.position, (playerToMove.transform.position + (playerToMove.transform.right * direction.x + playerToMove.transform.forward * direction.y)), speed * Time.deltaTime);
	}
}
