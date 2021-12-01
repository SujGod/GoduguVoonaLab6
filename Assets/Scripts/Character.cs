using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int ammo;
    void Start()
    {
        health = 5;
        ammo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("This is the health " + health + "and this is the ammo count " + ammo);
        if (health <= 0)
        {
            Debug.Log("YOU DIED");
        }
        if (ammo <= 0)
        {
            Debug.Log("YOU'RE OUT OF AMMO");
        }
    }
}
