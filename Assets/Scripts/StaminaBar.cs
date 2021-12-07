using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private int maxStamina = 100;
    public int minStamina;
    public float currentStamina;
    public bool isRunning = false;

    private float originalSpeed;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private MovementControl mc;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        minStamina = 10;
        mc = GetComponentInParent<MovementControl>();
        originalSpeed = mc.speed;

    }

    

    private void Update()
    {
        if (currentStamina > minStamina && isRunning)
        {
            if (mc.speed == originalSpeed)
            {
                mc.speed = mc.speed * 2;
            }
            UseStamina(0.4f);
        } else
        {
            mc.speed = originalSpeed;
            isRunning = false;
        }
    }

    
    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        { 
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            if(regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenStamina());
        } 
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(5);
        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
