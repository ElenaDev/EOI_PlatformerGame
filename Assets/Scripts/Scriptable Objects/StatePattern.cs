using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// State Pattern: patrones de diseño
/// Vamos a ver enumerados (enum) con switch para hacer máquinas de estados
/// </summary>
public class StatePattern : MonoBehaviour
{
    //Un enumerado es una forma de darle nombre a variable tipo int
    public int direction;//0 norte, 1 este, 2 oeste, 3 sur
    public enum Direction { North, East, West, South};//Creación del enumerado
    public Direction myDirection;//Creación de la variable a partir del enumerado

    void Start()
    {
        //myDirection = Direction.North;//asignación de un valor(estado) a mi variable enum

        switch (myDirection)
        {
            case Direction.North:
                Debug.Log("Estoy mirando al norte");
                break;
            case Direction.East:
                Debug.Log("Estoy mirando al este");
                break;
            case Direction.West:
                Debug.Log("Estoy mirando al oeste");
                break;
            case Direction.South:
                Debug.Log("Estoy mirando al sur");
                break;
            default:
                break;
        }

        if(myDirection == Direction.North)
        {
            Debug.Log("Estoy mirando al norte");
        }
        else if(myDirection == Direction.South)
        {
            Debug.Log("Estoy mirando al sur");
        }
        else if (myDirection == Direction.West)
        {
            Debug.Log("Estoy mirando al oeste");
        }
        else if (myDirection == Direction.East)
        {
            Debug.Log("Estoy mirando al este");
        }

        //direction es una variable tipo int
        //al switch hay que meterle una variable
        //dentro del switch vamos a tener diferentes casos donde miramos si el valor de esa variable
        //coincide
        switch(direction)
        {
            case 1:
                Debug.Log("La variable vale 1");
                break;
            case 7:
                Debug.Log("La variable vale 7");
                break;
            case 17:
                Debug.Log("La variable vale 17");
                break;
                //En el default se mete si el valor de la variable no coincide con ninguno de los casos
            default:
                Debug.Log("Ni idea de qué valor tiene");
                break;
        }
    }

    void Update()
    {
        
    }
}
