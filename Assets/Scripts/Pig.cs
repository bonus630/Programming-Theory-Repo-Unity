// INHERITANCE
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pig : EnemyBase
{
    [SerializeField] private AudioClip pigSFX;
    [SerializeField] private AudioClip bossSFX;
    private int previewLife = 10;
    private AudioSource adu;
    private float furyTimer = 4f;
    private bool inFury = false;
    private bool start = false;
    private int flik = 0;
    private CircleCollider2D collider2D;
    // POLYMORPHISM
    protected override void Start()
    {
        this.life = 10;
        this.repulse = Vector2.right * 1000;
        adu = GetComponent<AudioSource>();
        collider2D = GetComponent<CircleCollider2D>();
        base.Start();
        speed = 0;
        Invoke("Appear", 0);
        
    }
    // POLYMORPHISM
    protected override void Update()
    {
        
            base.Update();
            if (frontColliding)
            {
                xDirection *= -1;
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y);
                anim.SetBool("Walk", true);
            }
            rg.linearVelocityX =  speed * xDirection;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().FooterTouching(coll))
            {
                Debug.Log("Boss Collider " + gameObject.name);
                //collider2D.enabled = false;
                coll.enabled = false;
                gameObject.layer = 0;
                Invoke("Restore", 1f);
                rg.AddForce(Vector2.left * 300);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(repulse, ForceMode2D.Impulse);
                anim.SetTrigger("Hit");
                this.life--;
                if (life < 1)
                    Destroy(gameObject, 0.2f);
                adu.PlayOneShot(pigSFX);
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                speed *= 2;
                Invoke("ToWalk", 5f);
            }
            //else
            //{
            //    GameManager.Instance.GameOver();
            //}
        }
    }
 
    private void ToWalk()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", true);
        speed /= 2;
    }
    private void Appear()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
        flik++;
        if (flik < 11)
            Invoke("Appear", 0.1f);
        else
        {
            speed = 5;
            coll.enabled = true;
            anim.SetBool("Walk", true);
            adu.PlayOneShot(bossSFX);
        }
    }

}
