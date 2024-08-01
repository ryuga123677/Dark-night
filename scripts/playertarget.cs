using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playertarget : MonoBehaviour
{

    public float health = 50f;
    private AudioSource[] aud;


    private void Start()
    {
        aud = GetComponents<AudioSource>();
    }
    // Method to apply damage
    public void TakeDamage(float amount)
    {

        //  StartCoroutine(StopParticleSystemAfterDelay(particlesys, 1f));
        aud[1].Play();
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
        Destroy(gameObject); // Destroy the target
    }
}
