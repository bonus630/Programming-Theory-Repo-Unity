using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject collectableEffect;
    [SerializeField] private AudioClip collectSFX;
    private AudioSource audioSource;
    public int Amount { get { return amount; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(collectSFX);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collectableEffect.SetActive(true);
            Destroy(gameObject, 0.12f);
            GameManager.Instance.TotalScore += Amount;
            GameManager.Instance.UpdateScore();
        }
    }
   
}
