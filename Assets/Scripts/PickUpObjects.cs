using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            //Ejecutamos la función AddCoins del GameManager porque el player acaba de coger una moneda
            //Y destruimos la moneda
            GameManager.gameManager.AddCoins();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Heart"))
        {
            playerHealth.AddHeart();
            Destroy(collision.gameObject);
        }
    }
}
