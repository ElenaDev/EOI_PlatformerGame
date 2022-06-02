using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Los Headers son cabeceras para que las variables queden organizadas en el editor de unity
    [Header("Velocity")]
    public int speed;
    public float acceleration;

    [Header("Raycast - Jump")]
    public Transform groundCheck;//punto de origen del raycast, que va a estar en los pies del personaje
    public LayerMask layerGround;//La capa donde va a estar el suelo, ya que vamos a hacer un raycast selectivo
    public float reyLenght;//longitud del rayo
    public bool isGrounded;//Booleana para saber si está tocando el suelo o no
    public float jumpForce;//la fuerza del salto

    [Header("Attack")]
    public GameObject colliderAttackRight;
    public GameObject colliderAttackLeft;

    bool jumpPressed;//booleana para saber si se ha pulsado el botón de saltar

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    float h;
    bool isAttacking;//Booleana para saber si está atacando o no el player

    //Varibles para el movimiento
    Vector2 targetVelocity;//va a guardar la velocidad a la que quiero mover el personaje (tenemos aceleración)
    Vector2 dampVelocity;//aquí vamos a guardar la velocidad actual del personaje

    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (playerHealth.death || playerHealth.damaged)
        {
            targetVelocity = Vector2.zero;
            return;//me salgo del update
        }

        if (!isAttacking)//si no estoy atacando
        {
            h = Input.GetAxis("Horizontal");//AD
                                            // transform.Translate(h * Vector2.right * speed * Time.deltaTime);

            //targetVelocity representa la velocidad que quiero que lleve en el eje X (h*speed)
            //No toco el eje Y ya que eso viene controlado por el salto
            targetVelocity = new Vector2(h * speed, rb2D.velocity.y);
        }
        else//si estoy atacando
        {
            targetVelocity = Vector2.zero;
            h = 0;
        }


        //Estoy lanzando un raycast desde la posición groundCheck.position
        //con una dirección Vector2.down (es decir, eje Y hacia abajo)
        //con una longitud reyLenght
        //y es un raycast selectivo ya que solo va a detectar los gameobjects de la capa layerGround
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, reyLenght, layerGround);
        //Dibujamos el rayo en el panel scene:
        Debug.DrawRay(groundCheck.position, Vector2.down * reyLenght, Color.red);

        //En el update recogemos siempre los input
        //con la booleana jumpPressed, ya sé si el usuario ha pulsado el botón de saltar
        //Si pulsamos el botón de salto y el player está en el suelo -> puedo saltar
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isAttacking)
        {
            jumpPressed = true;
        }

        if(h!=0)//si el player se está moviendo
        {
            anim.SetBool("IsRunning", true);
        }
        else//si el player no se está moviendo
        {
            anim.SetBool("IsRunning", false);
        }
        //SpriteRenderer para cambiar el flip del personaje, es decir, hacia donde está mirando
        if(h>0)//si el personaje se mueve hacia la derecha
        {
            spriteRenderer.flipX = false;
        }
        else if(h<0)//Si el personaje se mueve hacia la izquierda
        {
            spriteRenderer.flipX = true;
        }

        //El parámetro del animator IsJumping va a valer lo contrario de la variable isGrounded
        anim.SetFloat("VelocityY", rb2D.velocity.y);
        anim.SetBool("IsJumping", !isGrounded);

        Attack();
        
    }
    private void FixedUpdate()
    {
        //Con SmoothDamp cambiamos el valor de un vector de forma gradual
        //Quiero cambiar el valor de velocidad de rigidbody(vector) hacia targetVelocity
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, targetVelocity, ref dampVelocity, acceleration);

        if(jumpPressed)
        {
            Jump();
        }
    }
    void Jump()
    {
        jumpPressed = false;
        rb2D.AddForce(Vector2.up * jumpForce);
    }
    void Attack()
    {
        if(Input.GetMouseButtonDown(0) && isGrounded)//si pulso el botón izquierdo del ratón
        {
            isAttacking = true;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);//parar al player diciendole que la velocidad que lleva es 0

            anim.SetTrigger("Attack");
            int n = Random.Range(0, 3);//esto me devuelve un número aleatorio entre 0 y 2 (porque el 3 no lo incluye)
            anim.SetInteger("SelectorAttack", n);
        }
    }

    //Función que va como evento en el último keyframe de las 3 animaciones de ataque
    public void IsAttackingToFalse()
    {
        isAttacking = false;
    }
    //Función que la vamos a usar como evento en la animación de ataque
    public void EnableCollider()
    {
        //si estoy mirando a la izquierda
        if(spriteRenderer.flipX) colliderAttackLeft.SetActive(true);
        //si estoy mirando a la derecha
        else if(!spriteRenderer.flipX) colliderAttackRight.SetActive(true);

        Invoke("DisableCollider", 0.1f);
    }
    void DisableCollider()
    {
        colliderAttackRight.SetActive(false);
        colliderAttackLeft.SetActive(false);
    }
}
