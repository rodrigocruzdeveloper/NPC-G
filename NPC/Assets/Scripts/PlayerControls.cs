using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Move Settings")]
    [Space]
    [SerializeField] private float speed;

    [Header("Attack Settings")]
    [Space]
    [SerializeField] private GameObject attackObject;


    private Vector2 direction;
    private bool rolling;
    private bool attack;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Collider2D collider2D;

    private Collider2D attackCollider;


    void Awake()
    {
        attackCollider = attackObject.GetComponent<Collider2D>();
        attackCollider.enabled = false;

        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        rigidbody2D.gravityScale = 0.0f;
        rigidbody2D.freezeRotation = true;
    }

    
    void Update()
    {
        Move();
        Roll();
        Attack();

        Animations();
    }


    void FixedUpdate()
    {
        OnMove();
    }


    void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(direction.x > 0)
        {
            transform.localScale = new Vector2(direction.x, 1);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector2(direction.x, 1);
        }
    }


    void OnMove()
    {
        rigidbody2D.linearVelocity = direction.normalized * speed;
    }


    void Roll()
    {
        rolling = Input.GetButtonDown("Jump");

        if(rolling == true)
        {
            StartCoroutine(Invencible());
        }
    }


    IEnumerator Invencible()
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(0.75f);
        collider2D.enabled = true;
        rolling = false;
    }


    void Attack()
    {
        attack = Input.GetButtonDown("Fire1");

        if (attack)
        {
            StartCoroutine(OnAttack());
        }
    }


    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        attackCollider.enabled = false;        
    }


    void Animations()
    {
        animator.SetInteger("pMove", (int)(Mathf.Abs(direction.x) + Mathf.Abs(direction.y)));
        animator.SetBool("pRoll", rolling);
        animator.SetBool("pAttack", attack);       
    }

}
