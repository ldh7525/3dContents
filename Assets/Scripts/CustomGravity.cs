using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    //public static float globalGravity = -9.81f;

    Rigidbody rb;

    void OnEnable ()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate ()
    {
        Vector3 gravity = Physics.gravity * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
