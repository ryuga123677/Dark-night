using System;
using UnityEngine;
using System.Collections;

public class Targets : MonoBehaviour
{
    public float health = 50f;
    private ParticleSystem particlesys;
    private ParticleSystem particlesys2;
    private AudioSource[] aud;

    private void Start()
    {
        aud = GetComponents<AudioSource>();
        Transform particleSystemTransform = transform.Find("Electro hit");
        if (particleSystemTransform != null)
        {
            particlesys = particleSystemTransform.GetComponent<ParticleSystem>();
            if (particlesys != null)
            {
                particlesys.Stop(); // Ensure particle system is initially stopped
            }
            else
            {
                Debug.LogWarning("ParticleSystem component not found on 'Magic fire 1'.");
            }
        }
        Transform particleSystemTransform2 = transform.Find("RotatorPS2");
        if (particleSystemTransform2 != null)
        {
            particlesys2 = particleSystemTransform2.GetComponent<ParticleSystem>();
            if (particlesys2 != null)
            {
                particlesys2.Stop(); // Ensure particle system is initially stopped
            }
            else
            {
                Debug.LogWarning("ParticleSystem component not found on 'Magic fire 1'.");
            }
        }


    }

    // Method to apply damage
    public void TakeDamage(float amount)
    {
        particlesys.Play();
        aud[1].Play();
        StartCoroutine(StopParticleSystemAfterDelay(particlesys, 1f));
        health -= amount;
        Debug.Log(health);
        if (health <= 0f)
        {
            Die();
        }
       
    }
    private IEnumerator StopParticleSystemAfterDelay(ParticleSystem ps, float delay)
    {
        yield return new WaitForSeconds(delay);
        ps.Stop();
    }


    // Method to handle the target's death
    void Die()
    {
        // Detach particlesys2 from the current GameObject
        particlesys2.transform.SetParent(null);

        // Play the particle system
        particlesys2.Play();

        // Destroy the main GameObject
        Destroy(gameObject);

        // Destroy the detached particle system after it has finished playing
        Destroy(particlesys2.gameObject, particlesys2.main.duration);
    }
}
