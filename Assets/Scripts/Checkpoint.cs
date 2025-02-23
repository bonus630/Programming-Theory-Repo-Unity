using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool active = false;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !active)
        {
            anim.SetBool("checked", true);
            GameManager.Instance.SavePlayerStates(new PlayerStates(gameObject.transform.position,GameManager.Instance.TotalScore));
            active = true;
           
        }
    }
}
