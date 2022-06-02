using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            //Ejecutamos la funci�n AddCoins del GameManager porque el player acaba de coger una moneda
            //Y destruimos la moneda
            GameManager.gameManager.AddCoins();
            Destroy(collision.gameObject);
        }
    }
}
