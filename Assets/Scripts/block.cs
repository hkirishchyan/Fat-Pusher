using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Rigidbody))]
public class block : MonoBehaviour
{
    public Renderer mainMat;
    public float weight;
    public TextMeshPro text_Mesh;
    Rigidbody rb;
    public fatguy ft;

    void Start()
    {
        mainMat = gameObject.GetComponent<Renderer>();
        weight = Random.Range(0.1f, 1f);
        text_Mesh = GetComponentInChildren<TextMeshPro>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (weight > 0)
        {
            text_Mesh.text = Mathf.Round(weight * 100).ToString();
            rb.mass = weight * 30;
        }  
        mainMat.material.SetFloat("_Weight", weight);
        if (weight <= 0)
        {
            Destroy(gameObject);
        }
    }

   private void OnCollisionStay(Collision collision)
   {
        if (collision.transform.tag == "Player")
        {
            ft.setState = 2;
            weight -= 0.01f;
            if(ft.fat>-30)
            {
                ft.fat -= Mathf.Round(weight * 10);
                ft.isPushing = true;
            }
        }
        if (collision.transform.tag != "Player")
        {
            ft.setState = 1;
            ft.isPushing = false;
        }
   }
}
