using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FormulesDetection : MonoBehaviour
{
    public Vector3 point1;
    public Vector3 point2;
    
    public Vector3 orientation1; // Le vecteur d'orientation pour une droite peut etre normalized ou non. ca d
    public Vector3 orientation2;

    
    
    void Formules()
    {
        point1 = new Vector3(0, 1, 0);
        point2 = new Vector3(1, 0, 0);
        
        DetecterCollisionEntrePoint(point1, point2);

        orientation1 = new Vector3(0, 2, 0);
        orientation2 = new Vector3(2, 0, 0);

    }
    
    void DetecterCollisionEntrePoint(Vector3 point1, Vector3 point2)
    {
        if (point1 == point2)
        {
            Debug.Log("Collision entre point 1 et point 2");
        }
    }

    void DetecterCollisionEntreDroite(Vector3 point1, Vector3 orientation1, Vector3 point2, Vector3 orientation2)
    {
        // formule --> point1.x+(Alpha * orientation1.x) = point2.x+(Beta * orientatio2.x)
        // formule --> point1.y+(Alpha * orientation1.y) = point2.y+(Beta * orientatio2.y)
        // trouver Alpha et Beta pour trouver le point ou la collision est possible.
        // si infinité de solution --> droite parallèle
        // si 0 solution --> droite perpendiculaire 
        // ensuite comparer les valeurs de Alpha et Beta avec la fomule --> point1.z+(Alpha * orientation1.z) == point2.z+(Beta * orientatio2.z) pour voir si la collision est possible.
        float alpha;
        float beta;
        
        // 1 : (alpha * orientation1.x) - (beta * orientation2.x) = point2.x - point1.x;
        // 2 : (alpha * orientation1.y) - (beta * orientation2.y) = point2.y - point1.y;
        
        // 1 : (alpha * orientation1.x)  = point2.x - point1.x + (beta * orientation2.x);
        // 1a: alpha = (point2.x - point1.x + (beta * orientation2.x))/orientation1.x
        
        // 2 remplacé le alpha par 1a: (alpha * orientation1.y) - (beta * orientation2.y) = point2.y - point1.y;

    }
}
