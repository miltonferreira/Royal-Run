using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField] float checkpointTimeExtension = 5f;

    GameManager gameManager;
    
    #region obstaculo
    ObstacleSpawner obstacleSpawner;
    [SerializeField] float obstacleDecreaseTimeAmount = .2f;
    #endregion

    const string playerString = "Player";

    void Start(){
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag(playerString)){
            gameManager.IncreaseTime(checkpointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }
}
