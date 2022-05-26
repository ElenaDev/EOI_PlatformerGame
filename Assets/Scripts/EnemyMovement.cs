using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed;
    public Vector2 leftLimit;//para el movimiento del enemigo, el límite por la izquierda
    public Vector2 rightLimit;//límite por la derecha
    public float distanceToAttackPlayer;//La distancia a la que voy a atacar al player
    public float xFactor;//La distancia a la que se va a quedar el enemigo del player cuando vaya a atacarlo

    Animator anim;
    Vector3 posToGo;//variable que siempre va a guardar la posición a la que se dirige el enemigo
    SpriteRenderer spriteRenderer;
    GameObject player;
    EnemyHealth enemyHealth;
    bool attacking;//Nos dice si el enemigo está persiguiendo al player (puede o no estar atacándole)
    public bool animAttacking;//Nos dice si el enemigo está atacando al player y se está reproduciendo la animación de ataque

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        //Establezco una posición de partida a la que va en un primer momento
        //Solo cojo de esta posición el valor en X, en el eje Y le digo que se mantenga donde está
        posToGo = new Vector2(rightLimit.x, transform.position.y);
    }

    void Update()
    {
        //Si el enemigo está recibiendo daño o está muerto le digo que se salga del update para que así no ejecute movimiento
        if (enemyHealth.damaged || enemyHealth.death) return;
     
        //creo un variable local que se llama distanceToPlayer
        //y en esta variable me guardo la distancia que hay entre enemigo y player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= distanceToAttackPlayer) 
            Attack();//El enemigo está atacando al player
        else
            ChangePosition();//El enemigo está haciendo la patrulla

        //Usamos MoveTowards para mover un gameobject de un punto a otro
        //Aquí le estamos diciendo que se diriga a posToGo si no está reproduciendo la animación de ataque
        if(!animAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
        }
            

        Flip();
        Animating();
    }
    void Attack()//El enemigo está persiguiendo al player y/o atacando
    {
        attacking = true;
        //El xFactor es para que el enemigo se quede a una distancia cerca del player para que se pare y ataque
        float direction;
        if (player.transform.position.x > transform.position.x) direction = -1f;
        else direction = 1f;

        posToGo = new Vector2(player.transform.position.x + (xFactor * direction), transform.position.y);

        if(Vector2.Distance(transform.position, posToGo) < 0.3f)
        {
            anim.SetTrigger("Attack");
        }
    }
    void ChangePosition()//El enemigo está patrullando
    {
        attacking = false;
        //¿El enemigo ha llegado a su punto de destino?
        if(transform.position == posToGo)
        {
            //Si posToGo es el límite por la derecha, le digo que el nuevo valor que va a tomar es el límite por la izquierda
            //para que se diriga hacia el otro lado
            if(posToGo.x == rightLimit.x) posToGo = new Vector2(leftLimit.x, transform.position.y);
            else posToGo = new Vector2(rightLimit.x, transform.position.y);
        }
    }
    void Flip()
    {
        Vector3 target;
        if (attacking) target = player.transform.position;
        else target = posToGo;

        if (target.x > transform.position.x) spriteRenderer.flipX = false;
        else if (target.x < transform.position.x) spriteRenderer.flipX = true;
    }
    void Animating()
    {
        if (transform.position == posToGo) anim.SetBool("IsWalking", false);

        else anim.SetBool("IsWalking", true);//La posición a la que quiero ir es diferente de la posición en la que
                                            //estoy, por lo tanto, el enemigo está caminando
    }

    //PARA SABER SI SE ESTÁ REPRODUCIENDO O NO LA ANIMACIÓN DE ATAQUE
    //Vamos a llamar como evento a esta función al inicio de la animación de ataque
    public void AnimAttackingTrue()
    {
        animAttacking = true;
    }
    //Vamos a llamar como evento a esta función al final de la animación de ataque
    public void AnimAttackingFalse()
    {
        animAttacking = false;
    }
    //
}
