using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExit : MonoBehaviour
{
    public bool exitReached;
    private void OnTriggerEnter(Collider other)
    {
        //set boolean flag to true on player collide with exit
        exitReached = true;
    }
}
