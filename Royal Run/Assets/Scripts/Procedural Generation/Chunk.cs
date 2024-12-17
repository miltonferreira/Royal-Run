using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    // pedaço de caminho que gera as maças, moedas e barreira no chao

    [SerializeField] float[] lanes = {-3.529f, 0, 3.3f};
    [SerializeField] GameObject fencePrefab; // prefab da cerca no pedaço de caminho

    #region apple
    [SerializeField] GameObject applePrefab;
    [SerializeField] float appleSpawnChance = .3f; // chance de 30% respawn apple na cena
    #endregion

    #region coin
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float coinSpawnChance = .5f; // chance de 50% respawn coin na cena
    [SerializeField] float coinSeperationLength = 2f;
    #endregion

    #region  Dependency Injection
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    #endregion

    List<int> availableLanes = new List<int>{0, 1, 2};

    private void Start() {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    public void Init(LevelGenerator lg, ScoreManager sm){
        this.levelGenerator = lg;
        this.scoreManager = sm;
    }

    void SpawnFences(){

        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {

            if (availableLanes.Count <= 0) break;

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }

    }

    void SpawnApple(){

        if((Random.value > appleSpawnChance) || (availableLanes.Count <= 0)) return; // permite ou nao respawn de apple

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        
        GameObject newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
        newApple.GetComponent<Apple>().Init(levelGenerator);
    }

    void SpawnCoin(){

        if((Random.value > coinSpawnChance) || (availableLanes.Count <= 0)) return; // permite ou nao respawn de apple

        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++){
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            
            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
            newCoin.GetComponent<Coin>().Init(scoreManager);
        }

    }

    int SelectLane(){
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

}
