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

    public float maxHealth;
    public float maxAmmo;

    private Character player;
    public Text ammoDisplay;
    public Text healthDisplay;
    public Text keyDisplay;

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

        characterHealthSlider.value = health / maxHealth;
        ammoBarSlider.value = ammo / maxAmmo;

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

        if (health != player.health)
        {
            health = player.health;
        }

        if (key != player.keyObtained)
        {
            key = player.keyObtained;
        }

    }
}
