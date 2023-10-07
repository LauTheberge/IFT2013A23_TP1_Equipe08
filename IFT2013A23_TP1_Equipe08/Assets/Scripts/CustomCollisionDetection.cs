using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionDetection : MonoBehaviour
{
    public float sphereHalfSize = 0.1f; // Comme le cube Unity par défaut a une taille de 1x1x1.
    public GameObject plane; // Glissez et déposez l'objet Plane ici depuis l'inspecteur.
    private BasePhysic _physiqueUpdate;
    private void Start()
    {
        _physiqueUpdate = GetComponent<BasePhysic>();
    }
    
private void Update()
    {
        CheckCollisionWithPlane();
    }
    
    
    void CheckCollisionWithPlane()
    {
        // Prenez la position Y du bas du cube.
        float sphereBottom = transform.position.y - sphereHalfSize;

        // Pour un plan avec un MeshCollider, la position y du plan représente le "haut" du plan.
        float planeTop = plane.transform.position.y;

        // Si le bas du cube est inférieur ou égal au haut du plan, nous considérons qu'il y a collision.
        if (sphereBottom <= planeTop)
        {
            Debug.Log("Collision détectée Sphere!");
            HandleCollision();
        }
    }
    
    void HandleCollision()
    {
        _physiqueUpdate.velocity = Vector3.zero;
        
        // Positionnez le cube directement au-dessus du plan.
        transform.position = new Vector3(transform.position.x, plane.transform.position.y + sphereHalfSize , transform.position.z);
    }
}
