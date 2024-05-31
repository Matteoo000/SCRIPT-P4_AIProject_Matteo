using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet object
    public Transform firePoint; // Point from where the bullet is fired
    public float bulletForce = 500f; // Force of the bullet, increased to ensure it shoots correctly
    public float fireRate = 0.5f; // Rate of fire in bullets per second

    private float nextFireTime = 0f; // Time until next fire
    //test for github
    void Update()
    {
        // Check if the player can shoot and the fire button is pressed
        if (CanShoot() && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    bool CanShoot()
    {
        // Check if enough time has passed since the last shot
        return Time.time >= nextFireTime;
    }

    void Shoot()
    {
        // Check if firePoint is assigned
        if (firePoint == null)
        {
            Debug.LogError("Fire point is not assigned!");
            return;
        }

        // Check if bulletPrefab is assigned
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned!");
            return;
        }

        // Update the next fire time
        nextFireTime = Time.time + 1f / fireRate;

        // Instantiate a bullet object at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Bullet instantiated");

        // Check if the bullet has a Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Add a Rigidbody component if it's missing
            rb = bullet.AddComponent<Rigidbody>();
            Debug.Log("Rigidbody added to the bullet");
        }

        // Disable gravity on the Rigidbody
        rb.useGravity = false;

        // Apply force to the bullet to shoot it
        rb.AddForce(firePoint.forward * bulletForce);
        Debug.Log("Force applied to the bullet");
    }
}