using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("iswalking", true);

    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
}
