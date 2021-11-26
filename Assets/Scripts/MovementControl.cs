using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControl : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private GameObject playerToMove;
	private InputAction moveAction;


	public void Initialize(InputAction moveAction)
	{
		this.moveAction = moveAction;
		moveAction.Enable();
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
