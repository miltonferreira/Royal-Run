using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    // controla as variações de velocidade e gravidade

    [Header("References")]
    [SerializeField] CameraController cameraController;

    #region chunks/pedaço
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkPointChunkPrefab;
    #endregion

    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")][Tooltip("The amount of chunks we start with")]
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] int checkpointChunkInterval = 8;
    [SerializeField] Transform chunkParent;
    [Tooltip("Do not change length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f; // posição que chunk ira respawnar
    [SerializeField] float moveSpeed = 8f;

    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;

    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    List<GameObject> chunks = new List<GameObject>();
    int chunksSpawned = 0;

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update() {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount){

        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed); // controla velocidade min e max do player

        if(newMoveSpeed != moveSpeed){
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ); // controla gravidade min e max dos objetos

            // modifica a gravidade do Z dos obstaculos
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

            cameraController.ChangeCameraFOV(speedAmount);
        }

        
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {

        float spawnPositionZ = CalculateSpawnPositionZ();

        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        GameObject chunkToSpawn = ChooseChunkToSpawn(); // define se o chunk tem ou nao checkpoint

        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);

        // ???????????
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager); // pega este LevelGenerator e scoreManager

        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        if ((chunksSpawned % checkpointChunkInterval == 0) && chunksSpawned != 0){
            chunkToSpawn = checkPointChunkPrefab;
        }else{
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    void MoveChunks(){ // faz esteira pra tras dos blocos
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength){ // se Z do chunk for menor que camera
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
