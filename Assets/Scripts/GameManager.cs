using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public int TotalScore { get; set; }
    private PlayerStates states;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        states = new PlayerStates(gameObject.transform.position, 0);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore()
    {
        scoreText.text = TotalScore.ToString("0000");
    }
    public void SavePlayerStates(PlayerStates playerStates)
    {
        this.states = playerStates;
    }
    public PlayerStates LoadPlayerStates()
    {
        return this.states;
    }
    public void GameOver()
    {
        //Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }
}
public class PlayerStates
{
    public PlayerStates()
    {

    }
    public PlayerStates(Vector2 playerPosition, int points)
    {
        this.PlayerPosition = playerPosition;
        this.Points = points;
    }
    public Vector2 PlayerPosition { get; set; }
    public int Points { get; set; }
}
