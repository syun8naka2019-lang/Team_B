using UnityEngine;
using UnityEngine.InputSystem;

public class skillattack: MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 10f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bulletSpeed; 
    }

}
