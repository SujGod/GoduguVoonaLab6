using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
	[SerializeField] public float speed;
	[SerializeField] private GameObject playerToMove;
	private InputAction moveAction;
	

	public void Initialize(InputAction moveAction, InputAction speedAction)
	{
		this.moveAction = moveAction;

		//shift button being performed and cancelled 
		speedAction.performed += SpeedEnhanced;
		speedAction.canceled += ResetSpeed;

		moveAction.Enable();
		speedAction.Enable();
	}

	private void SpeedEnhanced(InputAction.CallbackContext obj)
	{
		//Doubles speed in staminabar class
		StaminaBar.instance.isRunning = true;
		 
	}

	private void ResetSpeed(InputAction.CallbackContext obj)
	{
		//if shift key is cancelled reset to normal speed
		StaminaBar.instance.isRunning = false;
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
