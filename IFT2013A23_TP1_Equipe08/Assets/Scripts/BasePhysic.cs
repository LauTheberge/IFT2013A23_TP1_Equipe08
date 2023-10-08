using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePhysic : MonoBehaviour
{
    public Vector3 force = new Vector3(0, -9.81f, 0);
    [SerializeField] public float mass = 1.0f;
    
    [SerializeField] public float frictionCoefficient = 0.1f; // Coefficient de frottement


    private Vector3 acceleration;
    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        
        Vector3 friction = -velocity.normalized * frictionCoefficient * mass * 9.81f;
        
        acceleration = (force + friction) / mass;
        velocity += acceleration * Time.deltaTime;
    }
    
    void ExternalForce(Vector3 externalForce)
    {
        force += externalForce;
    }
}