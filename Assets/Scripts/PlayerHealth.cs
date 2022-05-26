using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;//3 corazones
    public int currentHealth;

    public GameObject[] heartsUI;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackEnemy"))
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        //desactivo el corazón correspondiente
        heartsUI[currentHealth - 1].SetActive(false);
        //el enemigo nos quita 1 de vida
        currentHealth--;
    
        if (currentHealth <= 0) Death();
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
