using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float startTime = 5f;

    [SerializeField] GameObject gameOverUI;

    #region vida player
     public float playerLife = 1f;
    public Scrollbar scrollbar;
    #endregion

    float timeLeft;

    #region game over
    bool gameOver;
    public bool GameOver { get; private set; }
    #endregion

    void Start() {
        timeLeft = startTime;    
    }

    void Update()
    {
        DecreseTime();

        scrollbar.size = playerLife;
    }

    public void IncreaseTime(float amount){
        timeLeft += amount;
    }

    void DecreseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    public void PlayerGameOver(){
        gameOver = true;
        playerController.enabled = false;
        
        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void BlockRoom(){
        Application.OpenURL("https://store.steampowered.com/app/2660980/Block_Room/");
    }
}
