using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoduguVoona.Input;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
	[SerializeField] private MovementControl movementController;
	[SerializeField] private RotatePlayerControl rotatePlayerController;
	[SerializeField] private Gun gun;
	private PlayerInputAction inputScheme;

	private void Awake()
	{
		inputScheme = new PlayerInputAction();
		movementController.Initialize(inputScheme.Player.Move, inputScheme.Player.Speed);
		rotatePlayerController.Initialize(inputScheme.Player.RotatePlayer);
	}

	private void OnEnable()
	{
		var _ = new QuitHandler(inputScheme.Player.Quit);
		var gunHandler = new GunshotHandler(inputScheme.Player.FireGun, this.gun);

	}
}
