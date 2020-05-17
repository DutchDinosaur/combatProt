using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countChildren : MonoBehaviour {
    public int count;

    void Start() {
        count = transform.childCount;
        Debug.Log(count);
    }
}