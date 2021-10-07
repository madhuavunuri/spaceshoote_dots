using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System.Collections;

public class ECSManager : MonoBehaviour
{
    public static EntityManager manager;

    BlobAssetStore store;

    public GameObject playerPrefab;
    public GameObject playerBulletPrefab;

    [Header("Astroids Data")]
    public Vector3 spawnValues;
    public GameObject asteroidPrefab;
    public GameObject asteroidBlastPrefab;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float asteroidOffset;


    private bool gameOver;
    private bool restart;
    private int score;

    Entity player;
    Entity asteroids;
    public static Entity asteroidBreak;
    public static Entity playerbullet;
    // Start is called before the first frame update
    void Start()
    {
        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        player = GameObjectConversionUtility.ConvertGameObjectHierarchy(playerPrefab, settings);
        playerbullet = GameObjectConversionUtility.ConvertGameObjectHierarchy(playerBulletPrefab, settings);
        asteroids = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);
        asteroidBreak = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidBlastPrefab, settings);




        var playerInstance = manager.Instantiate(player);
        var position = new float3(0,0,-5);
        manager.SetComponentData(playerInstance, new Translation { Value = position });

        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
       
       /// if(Input.GetKeyDown(KeyCode.Space))
       // {
        //    SpawnLarge();
        //}
    }

    void OnDestroy()
    {
        store.Dispose();
    }

  
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                var instance = manager.Instantiate(asteroids);
               // print(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x) + " LLLL " + spawnValues.z);
                float x = UnityEngine.Random.Range(-spawnValues.x, spawnValues.x);
                float y = spawnValues.y;
                float z = spawnValues.z + asteroidOffset;
                var pos = new float3(x,y,z);
                manager.SetComponentData(instance, new Translation { Value = new float3(pos.x, pos.y, pos.z) });

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
              //  restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
       // scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
       // gameOverText.text = "Game Over!";
        gameOver = true;
    }

}
