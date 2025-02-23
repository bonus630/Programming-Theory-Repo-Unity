using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    public void StartButton_clicked()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;


    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Loaded Scene " + arg0.name);
        PlayerStates ps = GameManager.Instance.LoadPlayerStates();
        if (ps != null)
        {
            var player = GameObject.Find("Player");
            player.transform.position = ps.PlayerPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
