using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_script : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, 2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
        }
    }
}
