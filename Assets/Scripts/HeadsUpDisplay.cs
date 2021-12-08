using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public bool isFiring;
    public int ammo;
    public int health;
    public bool key;
    public int score;


    public float maxHealth;
    public float maxAmmo;

    private Character player;
    public Text ammoDisplay;
    public Text healthDisplay;
    public Text keyDisplay;
    public Text scoreDisplay;

    public GameObject ammoBarUI;
    public Slider ammoBarSlider;

    public GameObject characterhealthBarUI;
    public Slider characterHealthSlider;

    public GameObject keyBarUI;
    public Slider keySlider;
    // Start is called before the first frame update
    void Start()
    {
        ammoBarUI.SetActive(true);
        characterhealthBarUI.SetActive(true);

        player = GetComponentInParent<Character>();
        ammo = player.ammo;
        health = player.health;
        key = player.keyObtained;
        score = player.score;

        maxHealth = player.maxHealth;
        maxAmmo = player.maxAmmo;

        if (key)
        {
            keySlider.value = 1;
        }
        else
        {
            keySlider.value = 0;
        }

        ammoBarSlider.value = ammo / maxAmmo;
        characterHealthSlider.value = health / maxHealth;
    }

    

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        healthDisplay.text = health.ToString();
        scoreDisplay.text = score.ToString();

        if (key)
        {
            keyDisplay.text = "Yes";
            keySlider.value = 1;
        }
        else
        {
            keyDisplay.text = "No";
            keySlider.value = 0;
        }

        if (ammo != player.ammo)
        {
            ammo = player.ammo;
        }

        if (score != player.score)
        {
            score = player.score;
        }

        if (health != player.health)
        {
            health = player.health;
        }

        if (maxHealth != player.maxHealth)
        {
            maxHealth = player.maxHealth;
        }

        if (key != player.keyObtained)
        {
            key = player.keyObtained;
        }

        characterHealthSlider.value = health / maxHealth;
        ammoBarSlider.value = ammo / maxAmmo;

    }
}
