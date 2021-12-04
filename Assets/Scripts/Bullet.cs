using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Transform charTransform;
    // Start is called before the first frame update

    //create variables for audio sound to be played when collision occur
    //public static AudioClip hitSound;
    //public static AudioSource audioSource;

    void Start()
    {
        //initialize coin collided sound
        //audioSource = GetComponent<AudioSource>();
        //hitSound = Resources.Load<AudioClip>("oof_sound");
    }

    // Update is called once per frame
    void FixedUpdate()
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
            predator.ChangeColorOnHit();
        }
    }
}
