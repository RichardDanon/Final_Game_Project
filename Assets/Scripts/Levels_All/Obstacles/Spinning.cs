using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float rotationSpeed = 10f;

    private void Update()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);

        // Apply the new rotation
        transform.rotation = newRotation;
    }
}
