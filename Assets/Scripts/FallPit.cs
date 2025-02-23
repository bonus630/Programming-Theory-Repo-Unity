using UnityEngine;

public class FallPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("fll");
            GameManager.Instance.GameOver();
        }
    }
}
