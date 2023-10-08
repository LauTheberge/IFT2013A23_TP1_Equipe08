using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionDetection : MonoBehaviour
{
    // TODO : remove hard coded value for sphereRadius
    public float sphereRadius = 0.1f;
    public GameObject plane;
    private BasePhysic _physiqueUpdate;

    private void Start()
    {
        _physiqueUpdate = GetComponent<BasePhysic>();
    }

    private void Update()
    {
        CheckCollisionWithPlane();
        UpdateBallPosition();
    }

    void CheckCollisionWithPlane()
    {
        Vector3 sphereCenter = transform.position;
        float distanceToPlane = Vector3.Dot(plane.transform.up, sphereCenter - plane.transform.position);

        if (distanceToPlane <= sphereRadius)
        {
            Debug.Log("Collision detected with plane!");
            HandleCollision(distanceToPlane);
        }
    }

    void HandleCollision(float distanceToPlane)
    {
        // Calculate the penetration depth.
        float penetrationDepth = sphereRadius - distanceToPlane;

        // Calculate the correction vector to move the ball out of the plane.
        Vector3 correction = plane.transform.up * penetrationDepth;

        // Adjust the position of the ball.
        transform.position += correction;
        
        // Zero out the vertical velocity component.
        Vector3 velocity = _physiqueUpdate.velocity;
        velocity.y = 0f;
        _physiqueUpdate.velocity = velocity;
    }

    void UpdateBallPosition()
    {
        // Update the position of the ball based on its velocity.
        Vector3 velocity = _physiqueUpdate.velocity;
        transform.position += velocity * Time.deltaTime;
    }
}