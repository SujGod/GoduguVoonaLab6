using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    [SerializeField] private float speed;
    private float oldSpeed;
    private bool isSpeedIncreased;
    [SerializeField] private float rot_speed;
    private Vector3 direction;
    public FieldOfView fieldOfView;


    // Start is called before the first frame update
    void Start()
    {
        isSpeedIncreased = false;
        oldSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector3.zero)
        {
            direction = fieldOfView.GetNewDirection();
        }

        if (fieldOfView.IsPreyInVision())
        {
            ChasePrey(fieldOfView.WhichPrey());
        } 
        else
        {
            speed = oldSpeed;
            isSpeedIncreased = false;
            Wander();
        }

        Orient();

    }

    void Wander()
    {
        if (fieldOfView.AreAnyMiddleRaysHit())
        {
            if (fieldOfView.AreAllRaysHit())
            {
                RotateDirection();
            }
            else
            {
                direction = fieldOfView.GetNewDirection();
            }

        }
        transform.position += direction * speed * Time.deltaTime;
    }

    void Orient()
    {
        Quaternion rot_target = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot_target, rot_speed * Time.deltaTime);
    }

    void RotateDirection()
    {
        direction = transform.right;
    }
    
    private void ChasePrey(Collider other)
    {
        if (!isSpeedIncreased)
        {
            speed += 2;
            isSpeedIncreased = true;
        }

        direction = other.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Prey")
        {
            Destroy(collision.collider.gameObject);
            speed = oldSpeed;
            isSpeedIncreased = false;
        }
    }
}
