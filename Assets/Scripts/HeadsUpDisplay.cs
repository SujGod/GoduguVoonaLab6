using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public bool isFiring;
    public int ammo;
    public int health;

    public float maxHealth;
    public float maxAmmo;

    private Character player;
    public Text ammoDisplay;
    public Text healthDisplay;

    public GameObject ammoBarUI;
    public Slider ammoBarSlider;

    public GameObject characterhealthBarUI;
    public Slider characterHealthSlider;
    // Start is called before the first frame update
    void Start()
    {

        ammoBarUI.SetActive(true);
        characterhealthBarUI.SetActive(true);

        player = GetComponentInParent<Character>();
        ammo = player.ammo;
        health = player.health;

        maxHealth = player.maxHealth;
        maxAmmo = player.maxAmmo;

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

        if (ammo != player.ammo)
        {
            ammo = player.ammo;
        }

        if (health != player.health)
        {
            health = player.health;
        }

    }
}
