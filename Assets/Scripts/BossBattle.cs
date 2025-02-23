using UnityEngine;

public class BossBattle : MonoBehaviour
{
   
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossPoint;
    [SerializeField] private BatSpawner batSpawner;

    private bool monitor = false;
    private bool startBattle = false;
    private float distance = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(monitor && !startBattle)
        {
            distance = Vector2.Distance(player.gameObject.transform.position, bossPoint.position);
            audioSource.volume = 1 / distance;
            if (distance < 3)
            {
                audioSource.volume = 0.9f;
                startBattle = true;
                Instantiate(boss, bossPoint.position, boss.transform.rotation);
                batSpawner.startBattle = true;
            }
            Debug.Log(distance);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monitor = true;
            audioSource.resource = bossMusic;
            audioSource.Play();
        }
    }
}
