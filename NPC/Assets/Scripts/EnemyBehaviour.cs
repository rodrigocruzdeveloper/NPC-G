using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int life;

    private GameObject target;

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");        
    }
    
    
    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,
                                                 target.gameObject.transform.position,
                                                 speed * Time.deltaTime);
    }

    void Look()
    {
        if(target.gameObject.transform.position.x < transform.position.x)
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
        if(other.tag == "Hammer")
        {
            spriteRenderer.color = Color.red;


            // TRABALHAR O COMPORTAMENTO DE RECEBER ATAQUE
        }
    }

    void Damage()
    {

    }

}
