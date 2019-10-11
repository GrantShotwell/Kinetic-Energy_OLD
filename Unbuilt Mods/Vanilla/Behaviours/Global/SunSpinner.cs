using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpinner : MonoBehaviour {
    public Vector3 delta;
    public void Update() { transform.localRotation = Quaternion.Euler(delta * Time.time); }
    public void SetSpeed(float speed) { delta = delta.normalized * speed; }
}
