using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour {
    [SerializeField] float speed = 5;
    [SerializeField] float RunMultip = 1.5f;
    [SerializeField] float JumpForce = 50;

    Rigidbody rb;
    //Animator aninimator;
    //SpriteRenderer spriteRenderer;

    float spd;
    float tempX;

    bool IsGrounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        //aninimator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //aninimator.SetBool("Walking", true);
            //if (Input.GetAxis("Horizontal") < 0) spriteRenderer.flipX = true;
            //else spriteRenderer.flipX = false;
        }
        //else aninimator.SetBool("Walking", false);

        spd = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            spd *= RunMultip;
            //aninimator.SetBool("Running", true);
        }
        //else aninimator.SetBool("Running", false);
        

        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        rb.MovePosition(transform.position + new Vector3(inputVector.x * spd, 0, inputVector.y * spd));

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) rb.velocity += Vector3.up * JumpForce;
        
        if (transform.position.y < -10) transform.position = Vector3.up * 2;
    }

    void OnCollisionEnter(Collision Collision) {
        if (Collision.gameObject.tag == "Jumpable") IsGrounded = true;
    }

    void OnCollisionExit(Collision Collision) {
        if (Collision.gameObject.tag == "Jumpable") IsGrounded = false;
    }
}