using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomCollisionDetection : MonoBehaviour
{
    [SerializeField] AudioClip youWin;
    [SerializeField] AudioClip youLose;

    public float sphereRadius = 0.2f;
    public GameObject sphere;
    public List<GameObject> gameObjectsPlancher;
    public List<GameObject> gameObjectsMur;

    private BasePhysic _physiqueUpdatesphere;
    private Mover _mover;
    private bool playSound = true;

    AudioSource audioSource;


    private void Start()
    {
        _physiqueUpdatesphere = sphere.GetComponent<BasePhysic>();
        _mover = sphere.GetComponent<Mover>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckCollisionWithPlane();
        _physiqueUpdatesphere.CustomUpdate();
    }

    void CheckCollisionWithPlane()
    {
        foreach (GameObject plancher in gameObjectsPlancher)
        {
            Vector3 sphereCenter = sphere.transform.position;
            if (IsSphereCollidingWithOBB(sphereCenter, sphereRadius, plancher.GetComponent<MeshFilter>()))
            {
                // Debug.Log("Collision detected with plancher!");
                float distanceToPlancher = GetDistanceToPlane(sphereCenter, plancher.GetComponent<MeshFilter>());
                HandleCollisionPLancher(distanceToPlancher, plancher);
            }
        }

        foreach (GameObject mur in gameObjectsMur)
        {
            Vector3 sphereCenter = sphere.transform.position;
            if (IsSphereCollidingWithOBB(sphereCenter, sphereRadius, mur.GetComponent<MeshFilter>()))
            {
                // Debug.Log("Collision detected with mur!");
                float distanceToPlancher = GetDistanceToPlane(sphereCenter, mur.GetComponent<MeshFilter>());
                HandleCollisionMur(distanceToPlancher, sphereRadius, sphereCenter, mur.GetComponent<MeshFilter>());
            }
        }
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


    void HandleCollisionPLancher(float distanceToPlane, GameObject plancher)
    {
        // Calculate the penetration depth.
        float penetrationDepth = sphereRadius - distanceToPlane;

        // Calculate the correction vector to move the ball out of the plane.
        Vector3 correction = plancher.transform.up * penetrationDepth;

        // Adjust the position of the ball.
        sphere.transform.position += correction;

        // Zero out the vertical velocity component.
        Vector3 velocity = _physiqueUpdatesphere.velocity;

        velocity.y = -velocity.y / 2;

        if (velocity.y >= -0.5 && velocity.y <= 0.5)
        {
            // Debug.Log("velocity.y = 0");
            velocity.y = 0;
            _mover.SetGrounded(true);
        }
        else
        {
            _mover.SetGrounded(false);
        }

        _physiqueUpdatesphere.velocity.y = velocity.y;


        if (plancher.gameObject.tag == "Finish" && playSound)
        {
            startWinSequence();
        }
        else if (plancher.gameObject.tag == "WrongGoal" && playSound)
        {
            startLoseSequence();
        }
    }

    void startWinSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(youWin);
        playSound = false;
    }

    void startLoseSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(youLose);
        playSound = false;
    }

    void HandleCollisionMur(float distanceToPlane, float radius, Vector3 sphereCenter, MeshFilter filter)
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
        float distance = Vector3.Distance(closestPoint, sphereCenter);
        if (distance <= radius)
        {
            // Calculer la direction de poussée
            Vector3 pushDirection = (sphereCenter - closestPoint).normalized;

            // Calculer la quantité de déplacement nécessaire pour résoudre la collision
            float pushAmount = radius - distance;

            // Mettre à jour la position de la sphère
            Vector3 newSphereCenter = sphereCenter + pushDirection * pushAmount;

            // Pour l'exemple, nous supposons que vous avez accès à la transformée de la sphère pour mettre à jour sa position.
            sphere.transform.position = newSphereCenter;
        }
    }
}