using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private BasePhysic _physics;

    public float moveForce = 50f;
    public Camera mainCamera;
    private bool isGrounded = false;
    public GameObject sphere;

    void Start()
    {
        _physics = sphere.GetComponent<BasePhysic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 oppositeDirection = mainCamera.transform.forward;
                oppositeDirection.y = 0;

                oppositeDirection.Normalize();

                Vector3 moveForceVector = oppositeDirection * moveForce;


                _physics.AddForce(moveForceVector);
            }
            
            _physics.ApplyFriction();
            if (_physics.velocity.magnitude < 0.1f)
            {
                _physics.velocity = Vector3.zero;
            }
        }
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
}