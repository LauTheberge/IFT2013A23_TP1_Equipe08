using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePhysic : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    [SerializeField] public float mass = 1.0f;
    
    [SerializeField] public float frictionCoefficient = 0.5f; // Coefficient de frottement

    private Vector3 friction;
    private Vector3 acceleration;
    public Vector3 velocity;
    public Vector3 force;
    
    // Update is called once per frame
    public void CustomUpdate()
    {
        transform.position += velocity * Time.deltaTime;
        
        friction = -velocity.normalized * frictionCoefficient * mass * 9.81f;
        
        acceleration = (gravity + force + friction) / mass;
        velocity += acceleration * Time.deltaTime;
        force = Vector3.zero;
    }
    
   public void AddForce(Vector3 externalForce)
    {
        force += externalForce;
    }
    
    public void ApplyFriction()
    {
        friction = -velocity.normalized * frictionCoefficient * mass * 9.81f;
        force += friction;
    }
}