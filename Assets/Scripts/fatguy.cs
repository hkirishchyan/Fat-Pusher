using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Fatguy Controller")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class fatguy : MonoBehaviour
{
    #region Variables&Placeholders
    [Header("Placeholders")]
    [SerializeField] Rigidbody fatmanRigidbody;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Locomotion Switch")]
    public bool movement_Mode;
    public bool isPushing;

    [Header("Drag Locomotion")]
    [Range(1, 10)] public float horizontal_sensitivity = 2;

    [Header("Swipe Locomotion")]
    [Range(1f, 10f)] public float move_smooth;
    [Range(1f, 10f)] public float horizontal_shift;

    [Header("General Locomotion")]
    [Range(0f, 20f)] public float movementSpeed;
    [Range(1f, 10f)] public float rotation_speed;

    [Header("Controls")]
    public Swipe swipe;
    public Vector3 followObject;
    public float targetAngle;
    float verticalMovement;
    public bool start;

    [Header("Animations and Events")]
    [SerializeField] Animator animator;
    public int setState;
    public GameObject finish_scene;
    public GameObject fail_scene;

    [Header("Stats")]
    public float maxFat;
    public float fat;
    #endregion

    void Start()
    {
        targetAngle = transform.rotation.y;
        fatmanRigidbody = GetComponent<Rigidbody>();
        verticalMovement = transform.position.x;
        start = false;
        followObject = transform.position;
        animator = GetComponentInChildren<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        WeightLoss(fat);
        Locomotion();
        EventConditions();
    }

    public void Locomotion()
    {
        if (start)
        {
            Animator(setState);

            if (fatmanRigidbody.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, targetAngle, transform.rotation.z), Time.deltaTime * rotation_speed);
            }
            if (movement_Mode)
            {
                if (!isPushing) {
                    if (Input.GetKeyDown(KeyCode.A) || swipe.leftSwipe)
                    {
                        followObject -= transform.right * horizontal_shift;
                    }
                    else if (Input.GetKeyDown(KeyCode.D) || swipe.rightSwipe)
                    {
                        followObject += transform.right * horizontal_shift;
                    }
                }
                Vector3 horMove = move_smooth * (followObject - transform.position);
                Vector3 movePos = transform.forward * movementSpeed + new Vector3(horMove.x * transform.right.x, fatmanRigidbody.velocity.y, horMove.z * transform.right.z);
                Vector3 movement = new Vector3(movePos.x, fatmanRigidbody.velocity.y, movePos.z);
                fatmanRigidbody.velocity = movement;
            }
            else
            {
                verticalMovement = ((Input.mousePosition.x / Screen.width) - 0.5f) * horizontal_sensitivity;
                Vector3 movePos = (transform.forward + (transform.right * verticalMovement) / 4) * movementSpeed;
                Vector3 movement = new Vector3(movePos.x, fatmanRigidbody.velocity.y, movePos.z);
                fatmanRigidbody.velocity = movement;
            }
        }
        else
        {
            Animator(0);
        }
    }
    public void EventConditions()
    {
        if (fat <= 0 || transform.GetComponent<Rigidbody>().velocity.y < -5)
        {
            Animator(3);
            start = false;
            fail_scene.SetActive(true);
        }
    }
    public void WeightLoss(float fat)
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0, fat / 5);
    }
    public void Animator(int state)
    {
        animator.SetInteger("State",state);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "left_turner")
        {
            Turner("left");
            followObject = transform.position;
        }
        if (other.tag == "right_turner")
        {
            Turner("right");
            followObject = transform.position;
        }
        if (other.tag == "forward_turner")
        {
            Turner("forward");
            followObject = transform.position;
        }
        if (other.tag == "food")
        {
            if(fat < maxFat)
            {
                fat += 100;
            }
            Destroy(other.gameObject);
        }
        if (other.tag == "finish")
        {
            start = false;
            finish_scene.SetActive(true);
        }
    }
    private void Turner(string turn)
    {
        switch (turn)
        {
            case "left":
                targetAngle = - 90;
                break;
            case "right":
                targetAngle = 90;
                break;
            case "forward":
                targetAngle = 0;
                break;
        }
    }
}
