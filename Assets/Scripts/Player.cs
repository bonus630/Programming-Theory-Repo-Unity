using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject footer;

    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip hitSFX;

    private Rigidbody2D rg;
    private Animator anim;
    private WallCheck wallCheck;
    private CircleCollider2D footerCollider;
    private AudioSource audioSource;


    private bool inGround;
    private bool isJumping;
    private bool doubleJump;
    private bool readyToJump;
    //private bool isStartJumpTimer;

    //private float jumpTimeCharger;



    public GameObject FooterColliding { get; protected set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        footerCollider = footer.GetComponent<CircleCollider2D>();
        wallCheck = GetComponent<WallCheck>();
        audioSource = GetComponent<AudioSource>();
        //jumpTimeCharger = startJumpTime;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && inGround)
        {
            isJumping = true;
        }
        else if (Input.GetButtonUp("Jump") && rg.linearVelocityY > 0)
        {
            rg.linearVelocityY *= 0.2f;
            doubleJump = true;
        }
        else if (Input.GetButtonUp("Jump") && doubleJump)
        {
            readyToJump = true;
        }
    }
    void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");
        Move(direction);
        Jump();
        //FooterColliding = Physics2D.Linecast(r.transform.position, l.transform.position) && isJumping;
        //Debug.Log(FooterColliding);
    }
    private void Jump()
    {
        if (isJumping)
        {
            rg.linearVelocityY = jumpForce;
            audioSource.PlayOneShot(jumpSFX);
            isJumping = false;
        }
        if (readyToJump)
        {

            rg.linearVelocityY = jumpForce;
            doubleJump = false;
            readyToJump = false;
            anim.SetTrigger("DoubleJump");
        }

        //if(isStartJumpTimer)
        //{
        //    jumpTimeCharger += Time.deltaTime;

        //}
        //if(Input.GetButtonDown("Jump"))
        //{
        //    isStartJumpTimer = true;


        //}
        //if (Input.GetButtonUp("Jump") || jumpTimeCharger >= 1)
        //{
        //    isStartJumpTimer = false;
        //   readyToJump = true;

        //}
        //if(readyToJump)
        //{

        //    if (!isJumping)
        //    {

        //        rg.AddForce(new Vector2(0, jumpForce*jumpTimeCharger), ForceMode2D.Impulse);
        //        doubleJump = true;

        //    }
        //    else
        //    {

        //        if (doubleJump)
        //        {
        //            rg.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //            doubleJump = false;
        //            anim.SetTrigger("DoubleJump");
        //        }
        //    }
        //    readyToJump = false;
        //    jumpTimeCharger = startJumpTime;
        //}
    }
    private void Move(float direction)
    {

        if (direction == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            //Debug.Log("R: " + wallCheck.RightWallCheck() + " " + direction);
           // Debug.Log("L: " + wallCheck.LeftWallCheck() + " " + direction);
            bool canMove = true;
            transform.eulerAngles = new Vector3(0, direction < 0 ? 180 : 0, 0);
            if (direction > 0 && wallCheck.RightWallCheck())
            {
                canMove = false;
            }
            if (direction < 0 && wallCheck.RightWallCheck())
            {
                canMove = false;
            }

            if (canMove)
            {
                anim.SetBool("Walk", true);

                //Vector3 moviment = new Vector3(direction, 0, 0);
                //transform.position += moviment * Time.deltaTime * speed;
                rg.linearVelocityX = speed * direction;

            }
            else
            {
                anim.SetBool("Walk", false);

                rg.linearVelocityX = 0;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
          //  Debug.Log("G");
            inGround = true;
            doubleJump = false;
            anim.SetBool("Jump", false);
        }
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 6)
        {
           // if (collision.gameObject.GetComponent<EnemyBase>().IsEnable)
          //  {
                Hit();
                Invoke("GameOver", 0.24f);
           // }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            inGround = false;
            anim.SetBool("Jump", true);
        }

    }
    private void Hit()
    {
        anim.SetTrigger("Hit");
        audioSource.PlayOneShot(hitSFX);
    }
    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
    public bool FooterTouching(Collider2D collision)
    {
        return footerCollider.IsTouching(collision);
    }

}
