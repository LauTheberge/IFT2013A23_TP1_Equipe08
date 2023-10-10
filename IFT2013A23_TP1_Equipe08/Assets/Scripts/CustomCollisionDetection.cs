using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionDetection : MonoBehaviour
{
    // TODO : SCRAP THIS FILE
    public float sphereRadius = 0.2f;
    public GameObject plane;
    private BasePhysic _physiqueUpdate;

    private void Start()
    {
        _physiqueUpdate = GetComponent<BasePhysic>();
    }

    private void Update()
    {
        CheckCollisionWithPlane();
        // UpdateBallPosition();
    }

    void CheckCollisionWithPlane()
    {
        Vector3 sphereCenter = transform.position;
        float distanceToPlane = GetDistanceToPlane(sphereCenter, plane.GetComponent<MeshFilter>());
        
        
        if (IsSphereCollidingWithOBB(sphereCenter, sphereRadius, plane.GetComponent<MeshFilter>()))
        {
            Debug.Log("Collision detected with plane!");
            HandleCollision(distanceToPlane);
        }
    }
    
    List<Vector3> GetVerticesOBBPlan()
    {
        // Trouvez tous les GameObjects avec un MeshFilter dans la scène
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        List<Vector3> verticesOBB = new List<Vector3>();
        
        foreach (MeshFilter filter in meshFilters)
        {
            Bounds bounds = filter.mesh.bounds;
            
            // Coins de l'AABB dans l'espace local
            Vector3 localFrontBottomLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
            Vector3 localFrontBottomRight = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            Vector3 localFrontTopLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            Vector3 localFrontTopRight = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
            Vector3 localBackBottomLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
            Vector3 localBackBottomRight = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
            Vector3 localBackTopLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
            Vector3 localBackTopRight = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
            
            // Transformez les coins de l'AABB pour obtenir l'OBB en coordonnées mondiales
            Vector3 frontBottomLeft = filter.transform.TransformPoint(localFrontBottomLeft);
            Vector3 frontBottomRight = filter.transform.TransformPoint(localFrontBottomRight);
            Vector3 frontTopLeft = filter.transform.TransformPoint(localFrontTopLeft);
            Vector3 frontTopRight = filter.transform.TransformPoint(localFrontTopRight);
            Vector3 backBottomLeft = filter.transform.TransformPoint(localBackBottomLeft);
            Vector3 backBottomRight = filter.transform.TransformPoint(localBackBottomRight);
            Vector3 backTopLeft = filter.transform.TransformPoint(localBackTopLeft);
            Vector3 backTopRight = filter.transform.TransformPoint(localBackTopRight);
            
            verticesOBB.Add(frontBottomLeft);
            verticesOBB.Add(frontBottomRight);
            verticesOBB.Add(frontTopLeft);
            verticesOBB.Add(frontTopRight);
            verticesOBB.Add(backBottomLeft);
            verticesOBB.Add(backBottomRight);
            verticesOBB.Add(backTopLeft);
            verticesOBB.Add(backTopRight);
        }
        return verticesOBB;
    }
    bool IsSphereCollidingWithOBB(Vector3 sphereCenter, float radius, MeshFilter filter)
    {
        Bounds bounds = filter.mesh.bounds;

        // Convertir le centre de la sphère en espace local de l'OBB
        Vector3 localSphereCenter = filter.transform.InverseTransformPoint(sphereCenter);

        // Trouver le point le plus proche sur l'AABB en espace local
        Vector3 closestPoint = new Vector3(
            Mathf.Clamp(localSphereCenter.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(localSphereCenter.y, bounds.min.y, bounds.max.y),
            Mathf.Clamp(localSphereCenter.z, bounds.min.z, bounds.max.z)
        );

        // Convertir le point le plus proche en coordonnées mondiales
        closestPoint = filter.transform.TransformPoint(closestPoint);

        // Vérifier si ce point est à l'intérieur de la sphère
        return Vector3.Distance(closestPoint, sphereCenter) <= radius;
    }

    float GetDistanceToPlane(Vector3 sphereCenter, MeshFilter filter)
    {
        Bounds bounds = filter.mesh.bounds;

        // Convertir le centre de la sphère en espace local de l'OBB
        Vector3 localSphereCenter = filter.transform.InverseTransformPoint(sphereCenter);

        // Trouver le point le plus proche sur l'AABB en espace local
        Vector3 closestPoint = new Vector3(
            Mathf.Clamp(localSphereCenter.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(localSphereCenter.y, bounds.min.y, bounds.max.y),
            Mathf.Clamp(localSphereCenter.z, bounds.min.z, bounds.max.z)
        );

        // Convertir le point le plus proche en coordonnées mondiales
        closestPoint = filter.transform.TransformPoint(closestPoint);

        // Vérifier si ce point est à l'intérieur de la sphère
        return Vector3.Distance(closestPoint, sphereCenter);
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

    // void UpdateBallPosition()
    //{
        // Update the position of the ball based on its velocity.
        // Vector3 velocity = _physiqueUpdate.velocity;
        // transform.position += velocity * Time.deltaTime;
    // }
}