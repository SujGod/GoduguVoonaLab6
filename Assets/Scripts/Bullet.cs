using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (transform.forward * Time.deltaTime * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collide with a barrier turn to a new direction
        if (collision.collider.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        else if (collision.collider.tag == "Predator")
        {
            //Do code for subtracting from Predators Health
            Destroy(this.gameObject);
            Predator predator = (Predator)collision.collider.GetComponent(typeof(Predator));
            predator.health--;
        }
    }
}
