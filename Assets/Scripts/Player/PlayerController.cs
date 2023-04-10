using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

   //Variáveis do Start() 
   private Rigidbody2D rb;
   private Animator anim;
   private Collider2D coll;

   //Máquina de Estados
   private enum State {idle, running, jumping, falling, hurt}
   private State state = State.idle;

   
   //Variáveis para o UI
   [SerializeField] private LayerMask ground;
   [SerializeField] private float speed = 5f;
   [SerializeField] private float jumpForce = 10f;
   [SerializeField] private int nanobots = 0;
   [SerializeField] private TextMeshProUGUI nanoText;
   [SerializeField] private float hurtForce = 10f;
   [SerializeField] private int health = 5;   
   [SerializeField] private TextMeshProUGUI healthAmount;
   [SerializeField] private GameObject portal;
   [SerializeField] private AudioSource getNut;
   [SerializeField] private AudioSource jump;
   [SerializeField] private AudioSource walk;
   [SerializeField] private AudioSource hurty;
   [SerializeField] private AudioSource crunch;
   [SerializeField] private int boots = 0;
   [SerializeField] private TextMeshProUGUI bootsText;
   [SerializeField] private GameObject bootsUI;
   [SerializeField] private int leek = 0;
   [SerializeField] private TextMeshProUGUI leekText;
   [SerializeField] private GameObject leekUI;
   [SerializeField] private GameObject secretPortal;
   [SerializeField] private CameraController camera;

   private bool canDoubleJump;
    private float hDirection;

    private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       coll = GetComponent<Collider2D>();
       healthAmount.text = health.ToString();
   } 

   private void Update()
   {
       if(state != State.hurt)
       {
            hDirection = Input.GetAxis("Horizontal");

            if(coll.IsTouchingLayers(ground))
            {
                canDoubleJump = true;
            }

            //Mover para a direita 
            if (hDirection < 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }


            //Mover para a direita 
            else if (hDirection > 0) 
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }

            if(boots > 0)
            {
                bootsUI.SetActive(true);
            }
            else
            {
                bootsUI.SetActive(false);
            }

            //Saltar 
            if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
            {
                Jump();
                jump.Play();
            }
            else
            {
                if (boots > 0 && canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    Jump();
                    jump.Play();
                    canDoubleJump = false;
                    boots -= 1;
                    bootsText.text = boots.ToString();
                }
            }
            if(leek > 0)
            {
                leekUI.SetActive(true);
            }
            if(health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
       }

       AnimationState();
       anim.SetInteger("state", (int)state); //coloca a animação dependendo do estado de Enumerador

       if(nanobots == 3)
       {
           portal.SetActive(true);
       }

       if(leek == 10)
       {
            secretPortal.SetActive(true);
       } 
   }

   private void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.tag == "Collectable")
       {
           getNut.Play(); 
           Destroy(collision.gameObject);
           nanobots += 1;
           nanoText.text = nanobots.ToString();
       }
       if(collision.tag == "Explosion")
       {
            camera.TriggerShake();
            state = State.hurt;
            hurty.Play();
            health -= 3;
            healthAmount.text = health.ToString();
       }
       if(collision.tag == "Boots")
       {
            getNut.Play();
            Destroy(collision.gameObject);
            boots += 1;
            bootsText.text = boots.ToString();
       }
       if(collision.tag == "Fall")
       {
           SceneManager.LoadScene(2);
       }
       if(collision.tag == "Fall2")
       {
           SceneManager.LoadScene(1);
       }
       if(collision.tag == "Fall3")
       {
           SceneManager.LoadScene(5);
       }
       if(collision.tag == "Fall1")
       {
           SceneManager.LoadScene(3);
       }
       if(collision.tag == "Tomato")
       {
            crunch.Play();
            health+=1;
            healthAmount.text = health.ToString();
            Destroy(collision.gameObject);
       }
       if(collision.tag == "Leek")
       {
            getNut.Play();
            Destroy(collision.gameObject);
            leek += 1;
            leekText.text = leek.ToString();
       } 
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
        if (other.gameObject.tag == "Obstacle")
        {
            camera.TriggerShake();
            state = State.hurt;
            //Trata da vida do jogador, atualizando o UI.
            hurty.Play();
            health -= 1;
            healthAmount.text = health.ToString();
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                //Obstáculo à direita por isso deveria perder vida e mover para a esquerda
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else
            {
                //Obstáculo à esquerda por isso deveria perder vida e mover para a direita
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }
        }
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                camera.TriggerShake();
                state = State.hurt;
                //Trata da vida do jogador, atualizando o UI.
                hurty.Play();
                health -= 1;
                healthAmount.text = health.ToString();
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    //Inimigo à direita por isso deverá perder vida e mover-se para a esquerda
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Inimigo à esquerda por isso deverá perder vida e mover-se para a direita
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        if(other.gameObject.tag == "Malagueta")
        {
            Enemy enemy2 = other.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                enemy2.JumpedOn();
                Jump();
            }
        }
    }


   private void AnimationState()
   {
       if(state == State.jumping)
       {
           if(rb.velocity.y < .1f)
           {
               state = State.falling;
           }
       }
       else if(state == State.falling)
       {
           if(coll.IsTouchingLayers(ground))
           {
               state = State.idle;
           }
       }
       else if (state == State.hurt)
       {
           if(Mathf.Abs(rb.velocity.x) < .1f)
           {
               state = State.idle;
           }
       } 
       else if(hDirection != 0)
       {
           //Moving
           state = State.running;
       }
       else
       {
           state = State.idle;
       }
   }

   private void footstep()
   {
       walk.Play();
   }
}
