using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceWakeUp;
    [SerializeField] private float distanceAttack;
    [SerializeField] private int life;

    private bool wakeUp;
    private bool attacking;

    private Animator animator;
    private SpriteRenderer spriteRender;


    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   
    void Update()
    {
        if (player == null) 
        {
            Debug.Log("Player Null.");
            return;
        }

        if (wakeUp == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceWakeUp)
            {
                wakeUp = true;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceAttack)
            {
                Attack();
            }
            else
            {
                Move();
            }            
        }

        AnimatorManager();

    }

    void Move()
    {
        if (attacking == true) return;


        transform.position = Vector2.MoveTowards(transform.position, 
                                                 player.transform.position, 
                                                 speed * Time.deltaTime);

        if(transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if(transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

    }

    void Attack()
    {
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        attacking = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }


    void AnimatorManager()
    {
        animator.SetBool("pWakeUp", wakeUp);
        animator.SetBool("pAttack", attacking);
        animator.SetBool("pDeath", life < 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       

        if(other.tag == "Hammer")
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        life--;

        if (life <= 0)
        {
            yield return new WaitForSeconds(3.0f);
            Destroy(gameObject, 2.0f);
        }
        else
        {
            spriteRender.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
    }


}
