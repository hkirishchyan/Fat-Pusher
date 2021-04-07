using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class cam_script : MonoBehaviour
{
    [Header("Placeholders")]
    public GameObject posHolder;
    public GameObject playerPos;
    [SerializeField] Rigidbody cam_rigidbody;

    [Header("Relative Position")]
    public Vector3 camPos;
    [SerializeField] private bool follow;
    [Range(1f, 10f)] public float move_smooth;

    void Start()
    {
        cam_rigidbody = GetComponent<Rigidbody>();
        follow = true;
    }

    void Update()
    {
        if (follow)
        {
            cam_rigidbody.velocity = move_smooth * ((posHolder.transform.position + camPos) - transform.position);
            cam_rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, playerPos.transform.rotation, 0.1f));
        }
        else
        {
            cam_rigidbody.useGravity = false;
            cam_rigidbody.velocity = Vector3.zero;
        }
        if (playerPos.GetComponent<Rigidbody>().velocity.y < -5)
        {
            follow = false;
        }
    }
}
