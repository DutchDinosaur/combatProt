using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkController : MonoBehaviour {

    [SerializeField] float distanceTreshold = 2;
    [SerializeField] float stepDistance = 1;
    [SerializeField] float stepSpeedAddition = 1;
    [SerializeField] IKLeg[] Leggs;
    [SerializeField] float stepTime = 1f;
    [SerializeField] float stepTimeMultip = 1f;
    float stp;
    [SerializeField] float ReiseHeight;

    Vector3[] Targets;
  
    private void Start() {
        Targets = new Vector3[Leggs.Length];
        for (int i = 0; i < Targets.Length; i++) {
            Targets[i] = Leggs[i].target;
        }

        StartCoroutine(SetSteps());
    }

    Vector3 previousPosition;
    Vector3 stepVector;

    void FixedUpdate() {
        stepVector = (transform.position - previousPosition).normalized * stepDistance + (transform.position - previousPosition) * stepSpeedAddition;
        //Debug.DrawLine(transform.position, transform.position + ((transform.position - previousPosition)*5), Color.black);

        for (int i = 0; i < Leggs.Length; i++) {
            Leggs[i].target += (Targets[i] - Leggs[i].target);
        }

        stp = stepTime - stepTimeMultip * stepVector.magnitude;
        previousPosition = transform.position;
    }

    IEnumerator SetSteps() {
        while (true) {
            doSteps(true);
            doReise(false);
            yield return new WaitForSeconds(stp);
            doSteps(false);
            doReise(true);
            yield return new WaitForSeconds(stp);
        }
    }

    void doSteps(bool even) {
        int startLeg;
        if (even) startLeg = 0;
        else startLeg = 1;

        for (int i = startLeg; i < Leggs.Length; i += 2)  {
            float distToTarget = Vector3.Distance(Leggs[i].Target.position, Leggs[i].target);

            if (distToTarget >= distanceTreshold) {

                RaycastHit hit;
                if (Physics.Raycast(new Ray(Leggs[i].Target.position + transform.up + stepVector, -transform.up), out hit, 2))  {
                    Targets[i] = hit.point;
                    //Debug.DrawLine(hit.point, hit.point + Vector3.up,Color.black);
                }
            }
        }
    }

    void doReise(bool even) {
        int startLeg;
        if (even) startLeg = 0;
        else startLeg = 1;

        for (int i = startLeg; i < Leggs.Length; i += 2) {
            float distToTarget = Vector3.Distance(Leggs[i].Target.position, Leggs[i].target);

            if (distToTarget >= distanceTreshold) {

                RaycastHit hit;
                if (Physics.Raycast(new Ray(Leggs[i].Target.position + transform.up, -transform.up), out hit, 2)) {
                    Targets[i] = hit.point + Vector3.up * ReiseHeight;
                    //Debug.DrawLine(hit.point + Vector3.up * ReiseHeight, hit.point + Vector3.up * ReiseHeight + Vector3.up, Color.black);
                }
            }
        }
    }
}