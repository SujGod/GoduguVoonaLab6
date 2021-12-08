using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunshotHandler
{
	private Gun gun;

	public GunshotHandler(InputAction fireGunAction, Gun gun)
	{
		fireGunAction.performed += FireGun_performed;
		fireGunAction.Enable();
		this.gun = gun;
	}

	private void FireGun_performed(InputAction.CallbackContext obj)
	{
		gun.FireBullet();
	}
}
