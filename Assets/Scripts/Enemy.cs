using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed;
    public Vector2 leftLimit;//para el movimiento del enemigo, el límite por la izquierda
    public Vector2 rightLimit;//límite por la derecha
    public float distanceToAttackPlayer;//La distancia a la que voy a atacar al player
    public float xFactor;//La distancia a la que se va a quedar el enemigo del player cuando vaya a atacarlo

    [Header("Colliders Attack")]
    public GameObject colliderAttackLeft;
    public GameObject colliderAttackRight;

    public enum State { Patrol,Following, Attacking};
    public State stateEnemy;

    Animator anim;
    Vector3 posToGo;//variable que siempre va a guardar la posición a la que se dirige el enemigo
    SpriteRenderer spriteRenderer;
    GameObject player;
    EnemyHealth enemyHealth;

    void Start()
    {
        stateEnemy = State.Patrol;

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
        if (enemyHealth.damaged || enemyHealth.death || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (stateEnemy)
        {
            case State.Patrol:
                {
                    if (distanceToPlayer <= distanceToAttackPlayer)
                        stateEnemy = State.Following;
                    ChangePosition();
                    break;
                }
            case State.Following:
                {
                    if (distanceToPlayer > distanceToAttackPlayer)
                        stateEnemy = State.Patrol;
                    else if (Vector2.Distance(transform.position, player.transform.position) < 3f)
                        stateEnemy = State.Attacking;                          
                    Following();
                    break;
                }
            case State.Attacking:
                {
                    if (distanceToPlayer > distanceToAttackPlayer)
                        stateEnemy = State.Patrol;
                    else if (Vector2.Distance(transform.position, player.transform.position) > 3f)
                        stateEnemy = State.Following;
                    Attack();
                    break;
                }
        }
        Flip();
        Animating();
    }
    void Following()//El enemigo está persiguiendo al player y/o atacando
    {
        //El xFactor es para que el enemigo se quede a una distancia cerca del player para que se pare y ataque
        float direction;
        if (player.transform.position.x > transform.position.x) direction = -1f;
        else direction = 1f;

        posToGo = new Vector2(player.transform.position.x + (xFactor * direction), transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
    void Attack()
    {
        anim.SetBool("Attack", true);
    }
    void ChangePosition()//El enemigo está patrullando
    {
        //¿El enemigo ha llegado a su punto de destino?
        if (transform.position == posToGo)
        {
            //Si posToGo es el límite por la derecha, le digo que el nuevo valor que va a tomar es el límite por la izquierda
            //para que se diriga hacia el otro lado
            if (posToGo.x == rightLimit.x) posToGo = new Vector2(leftLimit.x, transform.position.y);
            else posToGo = new Vector2(rightLimit.x, transform.position.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);
    }
    void Flip()
    {
        Vector3 target;
        if (stateEnemy != State.Patrol) target = player.transform.position;
        else target = posToGo;

        if (target.x > transform.position.x) spriteRenderer.flipX = false;
        else if (target.x < transform.position.x) spriteRenderer.flipX = true;
    }
    void Animating()
    {
        if (transform.position == posToGo) anim.SetBool("IsWalking", false);
        else anim.SetBool("IsWalking", true);//La posición a la que quiero ir es diferente de la posición en la que
                                             //estoy, por lo tanto, el enemigo está caminando

        if (stateEnemy != State.Attacking) anim.SetBool("Attack", false);
    }
    //Función que la vamos a usar como evento en la animación de ataque
    public void EnableCollider()
    {
        //si estoy mirando a la izquierda
        if (spriteRenderer.flipX) colliderAttackLeft.SetActive(true);
        //si estoy mirando a la derecha
        else if (!spriteRenderer.flipX) colliderAttackRight.SetActive(true);

        Invoke("DisableCollider", 0.1f);
    }
    void DisableCollider()
    {
        colliderAttackRight.SetActive(false);
        colliderAttackLeft.SetActive(false);
    }
}
