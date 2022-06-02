using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public GameObject[] heartsUI;

    public bool death;
    public bool damaged;

    Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
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
        if (currentHealth <= 0) return;
        //desactivo el corazón correspondiente
        heartsUI[currentHealth - 1].SetActive(false);

        damaged = true;
        //el enemigo nos quita 1 de vida
        currentHealth--;
        
        if (currentHealth <= 0) Death();
        else anim.SetTrigger("Hit");
    }
    //esta función va como evento en la animación de TakeHit, en el último keyframe, para volver
    //a poner a falso la booleana damaged
    public void DamagedToFalse()
    {
        damaged = false;
    }
    void Death()
    {
        death = true;
        anim.SetTrigger("Death");
        Destroy(gameObject,2);
    }

    void HeartsUI()
    {
        if (currentHealth == maxHealth) return;

        float x = currentHealth / maxHealth;
        float y = heartsUI.Length * x;

        float dec = y % 1;//Lo que tendría el corazón activo
        float num = (int)y;

        for(int i=0;i <=num;i++)
        {
            heartsUI[i].SetActive(true);
            if (i == num) heartsUI[i].GetComponent<Image>().fillAmount = dec;
        }
        if(num < heartsUI.Length -1 && num >0 )
        {
            for (int i = (int)num+1; i <= heartsUI.Length -1; i++)
            {
                heartsUI[i].SetActive(false);
            }
        }
    }
}
