using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image lifeUI;//Variable que va a hacer referencia a la imagen que tiene el enemigo como hija (esta imagen es
                        ////hija del canvas que tiene el enemigo como hijo)

    public TextMeshProUGUI textUI;//para mostrar los puntos de vida que ha quitado, lo tiene como hijo el enemigo
    public bool damaged;
    public bool death;
    public AnimationClip clipHit;//Clip de animación de daño

    Animator anim;
    float timer;
    EnemyMovement enemyMovement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AttackPlayer"))//si el collider de ataque del player entra en el collider del enemigo
        {
            TakeDamage();
        }
    }
    void TakeDamage()
    {
        if (death == true) return;//Si el enemigo está muerto no sigo mirando el resto de la función

        //Gestión del damage
        damaged = true;
        CancelInvoke("DamagedToFalse");
        Invoke("DamagedToFalse", clipHit.length);
        //

        currentHealth -= 10;
        lifeUI.fillAmount = currentHealth / maxHealth;//Actualizamos la barra de vida del enemigo
        //La división entre la salud actual y la máxima va a dar siempre un valor igual o menor a 1

        //Si no está reproduciendo la animación de ataque, le decimos que reproduzca la animación de daño
        if(!enemyMovement.animAttacking)
            anim.SetTrigger("Hit");

        textUI.gameObject.SetActive(true);
        textUI.transform.localPosition = Vector3.zero;
        textUI.text = "10";

        CancelInvoke("DesactivateTextUI");
        Invoke("DesactivateTextUI", 0.3f);

        if (currentHealth <= 0) Death();
    }
    void Death()
    {
        death = true;
        anim.SetTrigger("Death");
        Destroy(gameObject, 2);
    }
    void DesactivateTextUI()
    {
        textUI.gameObject.SetActive(false);
    }
    //Función la vamos a llamar como evento en la animación de Hit (Daño)
    public void DamagedToFalse()
    {
        damaged = false;
    }
}
