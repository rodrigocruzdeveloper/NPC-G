using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int life;
    [SerializeField] private float scale = 1;
    [SerializeField] private Color32 color;

    private GameObject target;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    private bool damage;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer.color = color;

        transform.localScale = new Vector2(scale, scale);
    }


    void Update()
    {
        Move();
        Look();
        Animations();   
    }

    void Move()
    {
        if (damage == true) return;       

        transform.position = Vector2.MoveTowards(transform.position,
                                                 target.gameObject.transform.position,
                                                 speed * Time.deltaTime);
    }

    void Look()
    {
        if (target.gameObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (target.gameObject.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hammer")
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        damage = true;

        GetComponent<Collider2D>().enabled = false;

        life--;

        if(life <= 0)
        {
            yield return new WaitForSeconds(0.9f);
            Destroy(gameObject);
        }

        for(int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.2f);
        }

        GetComponent<Collider2D>().enabled = true;
        spriteRenderer.color = color;

        damage = false;
    }

    void Animations()
    {
        animator.SetBool("pDamage", damage);
        animator.SetInteger("pLife", life);
    }


}
