using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    
    [SerializeField] float obstaclesSpawnTime = 1f;
    [SerializeField] float minObstaclesSpawnTime = .2f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f; // descola spawn para esquerda ou direita no X

    void Start(){
        StartCoroutine(SpawnObstacleRoutine());
    }

    // aumenta quantidade de obstaculos voadores na tela
    public void DecreaseObstacleSpawnTime(float amount){
        obstaclesSpawnTime -= amount;

        if(obstaclesSpawnTime <= minObstaclesSpawnTime){
            obstaclesSpawnTime = minObstaclesSpawnTime;
        }
    }

    IEnumerator SpawnObstacleRoutine(){
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

        yield return new WaitForSeconds(obstaclesSpawnTime);
        Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);

        StartCoroutine(SpawnObstacleRoutine());
    }
}
