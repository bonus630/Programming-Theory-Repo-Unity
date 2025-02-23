// INHERITANCE
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBat : EnemyBase
{
    public float startTime = 1f;
    private bool startFly = false;
    private AudioSource audioSource;
    [SerializeField] private Vector2 EndPoint;

    protected override void Start()
    {
        base.Start();
        Invoke("StartFly", 1);
        audioSource = GetComponent<AudioSource>();
    }
    protected override void Update()
    {
        base.Update();
        if (startFly)
        {
            if (startTime < 0)
                gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
            else
                startTime -= Time.deltaTime;
            if (gameObject.transform.position.x < EndPoint.x)
                Destroy(gameObject);
        }
        
    }
    private void StartFly()
    {
        startFly = true;
        audioSource.Play();
        anim.SetBool("StartFly", true);
    }
}
