using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public LayerMask Ground;

    private Rigidbody body;
    private Vector3 inputs = Vector3.zero;
    private bool isGrounded = true;
    private Transform groundChecker;

    private string[] attackAnimations = { "attack1", "attack2" };
    private Animator anim;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        groundChecker = transform.GetChild(6);
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);

        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
        {
            //transform.forward = inputs;
            transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);
            transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        if(Input.GetMouseButtonDown(0))
        {
            string attack = attackAnimations[Random.Range(0, attackAnimations.Length)];
            anim.SetTrigger(attack);
        }
    }

    public void rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
