using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsPosition : MonoBehaviour {

    [SerializeField] Transform hands;
    [SerializeField] float distance;
    [SerializeField] float handsHeight;

    Vector3 relativePos;

    void LateUpdate() {
        Vector2 rightStick = InputManager.instance.RightStick;
        if (rightStick.magnitude > .4f) {
            float angle = GetVector2Angle(rightStick);
            relativePos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized * distance + Vector3.up * handsHeight;
        }
        hands.LookAt(transform.position + Vector3.up * handsHeight);
        hands.position = relativePos + transform.position;
        transform.LookAt(hands.position + Vector3.down * handsHeight);
    }

    float GetVector2Angle(Vector2 vector) {
        return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg - 90;
    }
}