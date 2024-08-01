using UnityEngine;
using System.Collections;

public class fire : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public float range = 100f;
    public float damage = 10f;
    public Camera fpsCam;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public bool ispicked = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && ispicked)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 rayOrigin = fpsCam.transform.position;
        Vector3 rayDirection = fpsCam.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * range, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            Debug.Log(hit.transform.name);

            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            Targets target = hit.transform.GetComponent<Targets>();
            StartCoroutine(MoveProjectile(projectileInstance, hit.point, target));
        }
    }

    IEnumerator MoveProjectile(GameObject projectile, Vector3 targetPosition, Targets target)
    {
        float distanceTraveled = 0f;
        Vector3 startPosition = projectile.transform.position;

        while (projectile != null && Vector3.Distance(projectile.transform.position, targetPosition) > 0.1f)
        {
            // Calculate the movement step
            float step = projectileSpeed * Time.deltaTime;

            // Move the projectile
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, targetPosition, step);

            // Update the distance traveled
            distanceTraveled += step;

            // Check if the projectile has traveled beyond its range
            if (distanceTraveled >= 100f)
            {
                Destroy(projectile);
                yield break; // Exit the coroutine since the projectile is destroyed
            }

            yield return null;
        }

        // Destroy the projectile if it hasn't already been destroyed
        if (projectile != null)
        {
            Destroy(projectile);
        }

        // Apply damage to the target if present
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

}
