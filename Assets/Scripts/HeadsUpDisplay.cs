using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public bool isFiring;
    public int ammo;
    public int health;
    private Character player;
    public Text ammoDisplay;
    public Text healthDisplay; 
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Character>();
        ammo = player.ammo;
        health = player.health;
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        healthDisplay.text = health.ToString();
        if (ammo != player.ammo)
        {
            ammo = player.ammo;
        }

        if (health != player.health)
        {
            health = player.health;
        }

    }
}
