using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Predator : MonoBehaviour
{
    //public List<Prey> listOfPrey;
    public FOV fovPredator;
    private Vector3 direction;
    public int health;
    [SerializeField] private float maximumHealth;
    public GameObject healthBarUI;
    public Slider slider;

    public int preyListCount;

    [SerializeField] private float movementSpeed;
    private Material newMaterialWhenShot;
    private Material originalMaterial;

    void Start()
    {
        direction = Vector3.zero;
        health = (int)maximumHealth;
        newMaterialWhenShot = Resources.Load("HitColor", typeof(Material)) as Material;
        originalMaterial = this.GetComponent<Renderer>().material;
        slider.value = health / maximumHealth;
        healthBarUI.SetActive(true);
    }

    public void Update()
    {
        //get the list of prey that are in the field of vision
        List<Transform> preyList = fovPredator.FindVisiblePrey();

        //if there is one prey rotate towards the ray that hit it
        //move in the direction of the prey with a slight increase in speed
        if (preyList.Count == 1)
        {
            direction = (preyList[0].position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f);
            this.transform.position += direction * (movementSpeed * 1.5f) * Time.deltaTime;
        }

        //if there are multiple prey rotate towards an average of the positions of the prey
        //move in the direction of that average with a slight increase in speed
        else if (preyList.Count > 1)
        {
            var average = Vector3.zero;
            for (int i = 0; i < preyList.Count; i++)
            {
                average += preyList[i].position;
            }
            average = average / preyList.Count;

            direction = (average - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f);
            this.transform.position += direction * (movementSpeed * 1.5f) * Time.deltaTime;

        }

        //if there are no prey in view
        //wander through the scene as normal
        else
        {
            direction = fovPredator.GetNewDirection();
            this.transform.position += direction * movementSpeed * Time.deltaTime;
        }


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        slider.value = health / maximumHealth;
    }

    public void ChangeColorOnHit()
    {
        this.GetComponent<MeshRenderer>().material = newMaterialWhenShot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collide with a barrier turn to a new direction
        if (collision.collider.tag == "Wall")
        {
            direction = fovPredator.GetNewDirection();
        }

        //if collide with a prey destroy the prey object and remove it from the list
/*        if (collision.gameObject.tag == "Character")
        {
            *//* if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
                for (int i = 0; i < listOfPrey.Count; i++)
                {
                    if (listOfPrey[i].gameObject == collision.gameObject)
                    {
                        listOfPrey[i] = null;
                        listOfPrey.Remove(listOfPrey[i]);
                    }
                }
            }*//*
        }*/
    }
}
