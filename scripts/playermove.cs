using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playermove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float sensitivity = 0.1f;
    public Camera camera;
    float xrotation=0;
    public Transform orientation;
    // Update is called once per frame
    private void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput,0f, verticalInput) * speed * Time.deltaTime;

        // Apply movement to the player
        transform.Translate(movement);
       

    }
    private void FixedUpdate()
    {
        xrotation += -Input.GetAxis("Mouse Y") * 20f;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 20f, 0);

      //  orientation.rotation= Quaternion.Euler(0, yrotation, 0);
    }
    
}
