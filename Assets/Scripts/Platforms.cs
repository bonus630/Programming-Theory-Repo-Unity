using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private float fallingTime;

    private Animator anim;
    private Joint2D join;
    private BoxCollider2D coll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        join = GetComponent<Joint2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().FooterTouching(coll))
        {
            Invoke(nameof(DisablePlatform), fallingTime);
        }
    }
    private void DisablePlatform()
    {
        anim.SetBool("on", false);
        join.enabled = false;
        coll.isTrigger = true;
    }
}
