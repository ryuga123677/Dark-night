using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public GameObject stick;
    // Start is called before the first frame update
    public GameObject textpickup;
    private fire playerScript;
    void Start()
    {
        stick.SetActive(false);
        textpickup.SetActive(false);


        
    }
    private void OnTriggerStay(Collider other)
    { if (other.gameObject.tag == "player")
        {textpickup.SetActive(true);
            playerScript = other.GetComponent<fire>();
            if (Input.GetKey(KeyCode.E))
            {
                playerScript.ispicked = true;
                this.gameObject.SetActive(false);
                stick.SetActive(true);

            }
        }
    }


}
