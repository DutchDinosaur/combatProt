using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabing : MonoBehaviour {

    public FixedJoint GrabbedJoint;
    [SerializeField] float GrabRadius;
    [SerializeField] float holdDistance;
    [SerializeField] float moveToHandSpeed;

    CharacterController playerController;
    Rigidbody rb;

    private void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public void Grab() {
        Collider[] GrabColliders = Physics.OverlapSphere(transform.position, GrabRadius, LayerMask.GetMask("grab"));
        if (GrabColliders.Length > 0) {
            grabObject(GrabColliders);
        }
    }

    void grabObject(Collider[] GrabColliders) {
        FixedJoint joint = GrabColliders[0].transform.parent.gameObject.AddComponent<FixedJoint>() as FixedJoint;
        joint.connectedBody = rb;
        if (joint.gameObject.tag == "Weapon") {
            joint.transform.position = transform.position;
            playerController.swordRB = joint.GetComponent<Rigidbody>();
        }
        GrabbedJoint = joint;
    }

    public void Release() {
        GameObject.Destroy(GrabbedJoint);
        playerController.swordRB = null;
    }

    IEnumerator MoveToGrabber() {
        while (Vector3.Distance(GrabbedJoint.transform.position, transform.position) > holdDistance) {
            GrabbedJoint.transform.position = Vector3.Lerp(GrabbedJoint.transform.position, transform.position, Time.deltaTime * moveToHandSpeed);
            yield return new WaitForFixedUpdate();
        }

    }
}