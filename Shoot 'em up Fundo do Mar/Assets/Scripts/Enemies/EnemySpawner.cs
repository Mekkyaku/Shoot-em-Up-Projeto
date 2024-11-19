using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField]private float minimumSpawnTime;
    [SerializeField]private float maximumSpawnTime;
    private float timeUntilSpawn;

    void Awake(){
        SetTimeUntilSpawn();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerScript.gamePause){
            timeUntilSpawn -= Time.deltaTime;
            if(timeUntilSpawn <= 0){
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    }
