using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabing : MonoBehaviour {

    public FixedJoint GrabbedJoint;
    [SerializeField] float GrabRadius;
    [SerializeField] float BiggerGrabRadius;

    CharacterController playerController;
    Rigidbody rb;

    private void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Grab() {
        Collider[] GrabColliders = Physics.OverlapSphere(transform.position, GrabRadius, LayerMask.GetMask("grab"));
        if (GrabColliders.Length > 0) grabObject(GrabColliders);
    }

    void grabObject(Collider[] GrabColliders) {
        FixedJoint joint = GrabColliders[0].transform.parent.gameObject.AddComponent<FixedJoint>() as FixedJoint;
        if (joint.gameObject.tag == "Weapon") {
            joint.transform.position = transform.position;
            playerController.swordRB = joint.GetComponent<Rigidbody>();
        }
        joint.connectedBody = rb;
        GrabbedJoint = joint;
    }

    public void Release() {
        GameObject.Destroy(GrabbedJoint);
        playerController.swordRB = null;
    }
}