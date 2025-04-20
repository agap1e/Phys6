using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object1 : MonoBehaviour
{
    public bool coll = false;
    public Vector3 rot;

    private void OnCollisionEnter(Collision collision)
    {
        coll = true;
        rot = collision.gameObject.transform.forward;
        Debug.Log(rot);
    }
}
