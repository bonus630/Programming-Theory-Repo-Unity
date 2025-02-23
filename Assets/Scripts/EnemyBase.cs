// INHERITANCE
using UnityEngine;
// ABSTRACTION
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Transform topPoint;
    [SerializeField] protected Transform downPoint;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected BoxCollider2D coll;
    [SerializeField] protected float speed = 200;
    [SerializeField] protected int life = 1;
    protected Rigidbody2D rg;
    protected bool frontColliding;
    protected Animator anim;
    protected int xDirection = -1;
    protected Vector2 repulse = Vector2.up * 100;
    public bool IsEnable { get; set; } = true;
    // ABSTRACTION
    protected virtual void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // ABSTRACTION
    protected virtual void Update()
    {
        frontColliding = Physics2D.Linecast(topPoint.position, downPoint.position,layerMask);
        //if(frontColliding)
        //    Debug.Log(xDirection);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<Player>().FooterTouching(coll))
            {
                Debug.Log("Collider " + gameObject.name);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(repulse,ForceMode2D.Impulse);
                anim.SetTrigger("Hit");
                coll.enabled = false;
                gameObject.layer = 0;
                enabled = false;
                Invoke("Restore", 1f);
                this.life--;
                if(life < 1)
                    Destroy(gameObject,0.2f);
            }
            //else
            //{
            //    GameManager.Instance.GameOver();
            //}
        }
    }
    private void Restore()
    {
        enabled = false;
        coll.enabled = true;
        gameObject.layer = 6;
    }
}
