using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinSpeed = 10f;

    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the object is not affected by gravity
        rb.useGravity = false;

        rb.angularVelocity = Vector3.up * spinSpeed;
    }
}
