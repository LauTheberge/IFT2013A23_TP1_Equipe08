using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // TODO : Mettre notre physic dans le mover
    // 
    
    [SerializeField]float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       MovePlayer();
    }

    void PrintInstruction(string instruction)
    {
        Debug.Log(instruction);
    }

    void MovePlayer()
    {
       
    }
}
