using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Notre liste contenant tous les objets devant être gérés par le collision manager
    public GameObject[] _objectsList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Est-ce que notre sphère touche à quelque chose et si oui, quoi
        // analyse liste retournée par collisionListBuilder)

        List<GameObject> collisionList = collisionListBuilder();

        // Ajouter tag à tous les objets 
        // Aller chercher game object par tag

        //if (collisionList.Contains("Wall"))
        {
            //arrête
        }
        //else if (collisionList.Contains("Plane"))
        {
            //ne tombe pas
        }
        //else if(collisionList.Contains("GoalPlane"))
        {
            // appeler méthode checkGoal de game manager et ne pas tomber plus bas
        }
        //else if(collisionList.Contains("Cube"))
        {
            //glisse ou rebondit (pour l'instant elle va arrrêter)
        }
    }

    List<GameObject> collisionListBuilder()
    {
        // Aller chercher la liste de tous les objets (ObjectList)
        List<GameObject> collisionList = new List<GameObject>();
        // Itèrer sur objet x (s'assurer que toujour player) et objet y (x++)
        // Si déjà dans liste de collision, break (y suivant)
        // On apppelle collisionChecker sur les deux objets
        // Si true, ajouter dans liste de collision, break (y suivant)

        // On ne passe jamais au x suivant (car 

        // On garde ça simple - on ne rentre pas dans la gestion des arêtes. 
        return collisionList;
    }


    bool collisionChecker(GameObject obj1, GameObject obj2)
    {
        // obtient les sommets du premier object 
        // obtient les sommets du second object
        // vérifie si objet 1 est à l'intérieur d'objet 2
        return false;
    }
}