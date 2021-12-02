using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private ParticleSystem particleSystem;
    [SerializeField] Bullet bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet()
    {
        particleSystem.Play();
        Bullet bullet = Instantiate(bulletPrefab);

        bullet.transform.position = this.transform.position;
        bullet.transform.forward = this.transform.forward;

    }
}
