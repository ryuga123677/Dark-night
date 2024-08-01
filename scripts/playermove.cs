using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float sensitivity = 0.1f;
    public Camera cameras;
    private float xrotation = 0;
    public Transform orientation;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Apply movement to the player
        transform.Translate(movement);

        // Check if there is any movement input
        if (movement.magnitude > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        xrotation += -Input.GetAxis("Mouse Y") * 20f;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        cameras.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 20f, 0);

        // orientation.rotation= Quaternion.Euler(0, yrotation, 0);
    }
}
