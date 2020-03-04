using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float RunMultip;
    [SerializeField] float JumpForce;
    [SerializeField] float JumpFallForce;
    [SerializeField] float torqueMultip;

    [SerializeField] float holdMultiplier;
    [SerializeField] float holdHeightMultiplier;
    [SerializeField] float holdHeightOffset;
    [SerializeField] float lowerMultiplier;
    public Rigidbody swordRB;

    Rigidbody rb;

    float spd;

    bool IsGrounded;

    Vector3 handsRelativePos;
    float desiredAngle;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        //moovement
        spd = speed;
        if (InputManager.instance.Sprint) spd *= RunMultip;
        rb.AddForce(new Vector3(InputManager.instance.walkVector.x * spd, 0, InputManager.instance.walkVector.y * spd) * Time.deltaTime);

        //jump
        if (InputManager.instance.jump && IsGrounded) rb.AddForce(Vector3.up * JumpForce);
        if (rb.velocity.y > 0 && !InputManager.instance.jump) rb.AddForce(Vector3.down * JumpFallForce);

        //out of bounds
        if (transform.position.y < -10) transform.position = Vector3.up * 6;

        if (swordRB != null) sword();
    }

    void sword() {
        //hold
        if (InputManager.instance.hold > 0) {
            Vector3 force = new Vector3(InputManager.instance.AimVector.x, (1 - InputManager.instance.AimVector.magnitude) * holdHeightMultiplier + holdHeightOffset, InputManager.instance.AimVector.y) * holdMultiplier * Time.deltaTime;
            swordRB.AddForce(force);
            rb.AddForce(-force);
        }

        //slam
        if (InputManager.instance.lower > 0) swordRB.AddForce(Physics.gravity * InputManager.instance.lower * lowerMultiplier * Time.deltaTime);
    }

    private void FixedUpdate() {
        //rotate
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