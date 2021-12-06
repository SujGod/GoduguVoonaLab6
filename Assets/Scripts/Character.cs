using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth;
    public float maxAmmo;
    public int health;
    public int ammo;
    public bool keyObtained;
    private Maze maze;

    [SerializeField] private float knockbackRadius;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private GameObject text;
    void Start()
    {
        maze = new Maze();
        /* health = (int)maximumHealth;*/
        maxHealth = 5;
        health = (int)maxHealth;
        maxAmmo = 30;
        ammo = (int)(maxAmmo / 3);
        keyObtained = false;
        text.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("This is the health " + health + " and this is the ammo count " + ammo);
        if (health <= 0)
        {
            Debug.Log("YOU DIED");
            StartCoroutine(StopGameOverText());
        }
        if (ammo <= 0)
        {
            Debug.Log("YOU'RE OUT OF AMMO");
        }
    }

    public void ResetHealth()
    {
        health = (int)maxHealth;
        maxHealth = 5;
    }

    public void ResetAmmo()
    {
        ammo = (int)(maxAmmo / 3);
    }

    public void ResetKey()
    {
        keyObtained = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Predator")
        {
            health--;
            Vector3 direction = (collision.collider.transform.position - this.transform.position).normalized;
            if (InBounds())
            {
                transform.position += -transform.forward;
            }
        }
    }

    private bool InBounds()
    {
        if (this.transform.position.x < maze.sizeX / 2 && this.transform.position.x > -(maze.sizeX / 2) && this.transform.position.z < maze.sizeZ / 2 && this.transform.position.z > -(maze.sizeZ / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    IEnumerator StopGameOverText()
    {
        //display game over text for 10 seconds then turn it off
        text.SetActive(true);
        yield return new WaitForSeconds(2);
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }
}
