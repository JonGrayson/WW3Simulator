using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ballistics : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public float launchForce = 10f;
    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        Time.timeScale = myTimeScale;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FiringSolution firingSolution = new FiringSolution();
            Nullable<Vector3> aimVector = firingSolution.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
            if(aimVector.HasValue)
            {
                rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb.isKinematic = false;
        }
    }
}
