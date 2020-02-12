using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour {
    [SerializeField] float speed = 5;
    [SerializeField] float RunMultip = 1.5f;
    [SerializeField] float JumpForce = 50;

    [SerializeField] float torqueMultip;

    [SerializeField] float holdMultiplier;
    [SerializeField] float holdHeightMultiplier;
    [SerializeField] float lowerMultiplier;
    [SerializeField] Rigidbody swordRB;

    Rigidbody rb;

    float spd;

    bool IsGrounded;

    Vector3 handsRelativePos;
    float desiredAngle;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        spd = speed;
        if (InputManager.instance.Sprint) spd *= RunMultip;
        //rb.MovePosition(transform.position + new Vector3(InputManager.instance.walkVector.x * spd, 0, InputManager.instance.walkVector.y * spd));
        rb.AddForce(new Vector3(InputManager.instance.walkVector.x * spd, 0, InputManager.instance.walkVector.y * spd));

        if (InputManager.instance.jump && IsGrounded) rb.velocity += Vector3.up * JumpForce;
        
        if (transform.position.y < -10) transform.position = Vector3.up * 6;

        if (InputManager.instance.hold > 0) {
            Vector3 force = new Vector3(InputManager.instance.AimVector.x, (1 - InputManager.instance.AimVector.magnitude) * holdHeightMultiplier + 1, InputManager.instance.AimVector.y) * holdMultiplier;
            swordRB.AddForce(force);
            rb.AddForce(-force);
        }

        //if (InputManager.instance.hold > 0) swordRB.AddForce(-Physics.gravity * InputManager.instance.hold * holdMultiplier);
        if (InputManager.instance.lower > 0) swordRB.AddForce(Physics.gravity * InputManager.instance.lower * lowerMultiplier);
    }

    private void FixedUpdate() {
        desiredAngle = (InputManager.instance.AimVector.magnitude > .4f) ? GetVector2Angle(InputManager.instance.AimVector) : desiredAngle;
        float angle = transform.rotation.eulerAngles.y;

        rb.AddTorque(Vector3.up * AngleOffset(angle, desiredAngle) * torqueMultip);
    }

    void OnCollisionEnter(Collision Collision) {
        if (Collision.gameObject.tag == "Jumpable") IsGrounded = true;
    }

    void OnCollisionExit(Collision Collision) {
        if (Collision.gameObject.tag == "Jumpable") IsGrounded = false;
    }

    float GetVector2Angle(Vector2 vector) {
        return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
    }

    private float AngleOffset(float firstAngle, float secondAngle) {
        float difference = secondAngle - firstAngle;
        while (difference < -180) difference += 360;
        while (difference > 180) difference -= 360;
        return difference;
    }
}