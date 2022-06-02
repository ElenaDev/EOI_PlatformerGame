using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float minForceAmount;//m�nima fuerza con la que van a salir las monedas
    public float maxForceAmount;//m�xima fuerza con la que van a salir las monedas
    float forceAmount;
    public float torqueAmount;
    public int direction;//si vale 1 la moneda saldr� hacia la derecha
    //si vale -1 saldr� hacia la izquierda

    Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Random.value devuelve un valor entre 0 y 1
        if (Random.value < 0.5f) direction = 1;
        else direction = -1;

        forceAmount = Random.Range(minForceAmount, maxForceAmount);
        rb2D.AddForce(Vector3.up * forceAmount);
        rb2D.AddForce(Vector3.right * forceAmount/4 * direction);

        //le a�adimos fuerza de giro
        rb2D.AddTorque(torqueAmount * direction);
    }

    
}
