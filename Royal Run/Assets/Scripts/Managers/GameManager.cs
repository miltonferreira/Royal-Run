using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float startTime = 5f;

    [SerializeField] GameObject gameOverText;

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

    void PlayerGameOver(){
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = .1f;
    }
}
