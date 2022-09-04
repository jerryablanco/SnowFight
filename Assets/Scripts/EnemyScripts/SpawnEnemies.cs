using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private int totalNumberOfEnemies = 1;//10;
    [SerializeField]
    private int maxNumberOfEnemies = 2;//25;
    [SerializeField]
    private SpawnPoints spawnPoints;
    [SerializeField]
    private float initialSpawnDelay = 30f;
    [SerializeField]
    private float difficultyTimer = 15f;
    [SerializeField]
    private float minimumSpawnDelay = .5f;
    private float currentSpawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        InvokeRepeating("SpawnEnemy", 0.5f, currentSpawnDelay); 
        //StartCoroutine(SpawnTimerIncrease());
    }


    private void SpawnEnemy()
    {
        GameObject enemy = EnemyPool.enemyPoolInstance.GetEnemy();
        Vector2 spawnPoint = GetNewSpawnPoint();
        enemy.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0);
        enemy.SetActive(true);
    }

    private IEnumerator SpawnTimerIncrease()
    {
        yield return new WaitForSeconds(difficultyTimer);
        currentSpawnDelay = currentSpawnDelay - .1f;
        if(currentSpawnDelay < minimumSpawnDelay) {
            currentSpawnDelay = minimumSpawnDelay;
        }
        totalNumberOfEnemies++;
        if(totalNumberOfEnemies > maxNumberOfEnemies) {
            totalNumberOfEnemies = maxNumberOfEnemies;
        }
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0.1f, currentSpawnDelay);
    }


    //Todo: refactor to be vector 3 (was using different method before)
    private Vector2 GetNewSpawnPoint()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 13);
        return spawnPoints.locations[randomNumber];
    }

}
