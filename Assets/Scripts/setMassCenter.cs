using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class setMassCenter : MonoBehaviour {

    [SerializeField] Vector3 center;

    void Start() {
        GetComponent<Rigidbody>().centerOfMass = center;
    }
}