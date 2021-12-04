using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] Bullet bulletPrefab;
    private Character player;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
        player = GetComponentInParent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet()
    {
        if (player.ammo != 0)
        {
            particleSystem.Play();
            Bullet bullet = Instantiate(bulletPrefab);
            player.ammo -= 1;
            bullet.transform.position = this.transform.position;
            bullet.transform.forward = this.transform.forward;
        }
    }
}
