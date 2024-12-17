using UnityEngine;

public class Coin : Pickup {

    [SerializeField] int scoreAmount = 10;

    ScoreManager scoreManager;

    // injeção de depedencia
    public void Init(ScoreManager sm) {
        this.scoreManager = sm;
    }

    protected override void OnPickup(){
        scoreManager.IncreaseScore(scoreAmount);
    }
}
