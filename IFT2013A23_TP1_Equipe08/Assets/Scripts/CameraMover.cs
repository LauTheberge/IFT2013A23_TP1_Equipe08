using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5.0f;

    public float rotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

// Update is called once per frame

    void Update()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(xValue, 0, zValue);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed, 0);
        }
    }
}