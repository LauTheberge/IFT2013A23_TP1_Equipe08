using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePhysic : MonoBehaviour
{
    public Vector3 force = new Vector3(0, -9.81f, 0);
    [SerializeField] public float mass = 1.0f;
    
    [SerializeField] public float frictionCoefficient = 0.1f; // Coefficient de frottement


    private Vector3 acceleration;
    private Vector3 velocity;
    private Vector3 position;
    
    // Start is called before the first frame update
    void Start()
    {
       position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 friction = -velocity.normalized * frictionCoefficient * mass * 9.81f;
        
        acceleration = (force + friction) / mass;
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;
    }
    
    void ExternalForce(Vector3 externalForce)
    {
        force += externalForce;
    }
}