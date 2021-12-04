using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExit : MonoBehaviour
{
    public bool exitReached;
    /*    private void OnTriggerEnter(Collider other)
        {
            //set boolean flag to true on player collide with exit
            if (other.CompareTag("Character"))
            {
                Character player = (Character)other.GetComponent(typeof(Character));
                if (player.keyObtained)
                {
                    exitReached = true;
                }
            }
        }*/

    private void OnCollisionEnter(Collision collision)
    {
        //set boolean flag to true on player collide with exit
        if (collision.collider.CompareTag("Character"))
        {
            Character player = (Character)collision.collider.GetComponent(typeof(Character));
            if (player.keyObtained)
            {
                exitReached = true;
            }
        }
    }
}
