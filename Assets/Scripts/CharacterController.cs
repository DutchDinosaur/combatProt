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
        rb.MovePosition(transform.position + new Vector3(InputManager.instance.LeftStick.x * spd, 0, InputManager.instance.LeftStick.y * spd));

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) rb.velocity += Vector3.up * JumpForce;
        
        if (transform.position.y < -10) transform.position = Vector3.up * 2;

        if (InputManager.instance.hold > 0) swordRB.AddForce(-Physics.gravity * InputManager.instance.hold * holdMultiplier);
        if (InputManager.instance.lower > 0) swordRB.AddForce(Physics.gravity * InputManager.instance.lower * lowerMultiplier);
    }

    private void FixedUpdate() {
        desiredAngle = (InputManager.instance.RightStick.magnitude > .4f) ? GetVector2Angle(InputManager.instance.RightStick) : desiredAngle;
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
        return Mathf.Atan2(vector.x, vector.y) * -Mathf.Rad2Deg + 180;
    }

    private float AngleOffset(float firstAngle, float secondAngle) {
        float difference = secondAngle - firstAngle;
        while (difference < -180) difference += 360;
        while (difference > 180) difference -= 360;
        return difference;
    }
}