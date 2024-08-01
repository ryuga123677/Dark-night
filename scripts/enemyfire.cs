using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public float range = 100f;
    public float damage = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform playerTransform;
    public float fireInterval = 2f; // Time between shots
    private float nextFireTime;

  
    void Update()
    {
        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval; // Schedule the next shot
        }
    }

    void Shoot()
    {
        // Calculate the direction towards the player
        Vector3 directionToPlayer = playerTransform.position - firePoint.position;
        directionToPlayer.Normalize(); // Ensure the direction is a unit vector

        // Debugging ray to visualize the direction
        Debug.DrawRay(firePoint.position, directionToPlayer * range, Color.red, 1.0f);

        // Instantiate the projectile
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));

        // Start the coroutine to move the projectile
        StartCoroutine(MoveProjectile(projectileInstance, directionToPlayer));
    }

    IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float distanceTraveled = 0f;

        while (projectile != null)
        {
            // Calculate the step distance for this frame
            float step = projectileSpeed * Time.deltaTime;

            // Move the projectile
            projectile.transform.position += direction * step;

            // Update the distance traveled
            distanceTraveled += step;

            // Check if the projectile hits the player
            RaycastHit hit;
            if (Physics.Raycast(projectile.transform.position, direction, out hit, step))
            {
                Playertarget playerTarget = hit.collider.GetComponent<Playertarget>();
                if (playerTarget != null)
                {
                    playerTarget.TakeDamage(damage);
                    Destroy(projectile);
                    yield break; // Exit the coroutine since the projectile is destroyed
                }
            }

            // Destroy the projectile if it has traveled beyond its range
            if (distanceTraveled >= range)
            {
                Destroy(projectile);
                yield break;
            }

            yield return null;
        }
    }

}
