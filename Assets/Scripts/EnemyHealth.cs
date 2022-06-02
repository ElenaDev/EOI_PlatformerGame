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

    [Header("Dropping")]
    public GameObject coin;
    public GameObject heart;
    public int maxObjectsDropping;

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

        textUI.gameObject.SetActive(true);
        textUI.transform.localPosition = Vector3.zero;
        textUI.text = "10";

        CancelInvoke("DesactivateTextUI");
        Invoke("DesactivateTextUI", 0.3f);

        if (currentHealth <= 0) Death();
        else if (!enemyMovement.animAttacking) anim.SetTrigger("Hit");
    }
    void Death()
    {
        Dropping();
        death = true;
        anim.SetTrigger("Death");
        Destroy(gameObject, 2);
    }
    /// <summary>
    /// Genera monedas y corazones aleatorios
    /// HAY MENOS POSIBILIDADES QUE SALGAN CORAZONES QUE MONEDAS
    /// </summary>
    void Dropping()
    {
        int n = Random.Range(2, maxObjectsDropping);
        for(int i=0; i <= n; i++)
        {
            //Por cada objeto que creamos hay un probabilidad menor del 10% de que sea un corazón
            //new Quaternion() es decirle que no tiene rotación
            if (Random.value < 0.1f) Instantiate(heart, transform.position, new Quaternion());
            else Instantiate(coin, transform.position, new Quaternion());
        }
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
