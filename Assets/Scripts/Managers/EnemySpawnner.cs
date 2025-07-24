using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemySetup
{
    public GameObject prefabs;
    public float spawnHeight;
}
public class EnemySpawnner : MonoBehaviour, IObserver
{
    public List<EnemySetup> enemySetups;
    public int spawnPerUnit = 3;
    private PlayerScore playerScript;
    private int spawnRelativeToDiff;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag(ConstantsValue.PLAYER_TAG).GetComponent<PlayerScore>();

        SpawnUp(0);

        GameplayController.instance.RegisterObserver(this);
    }

    private void SpawnUp(int difficultyLevel)
    {
        spawnRelativeToDiff = spawnPerUnit + difficultyLevel;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ConstantsValue.BG_TAG)
        {
            float width = other.transform.localScale.x;
            Vector3 eulerAngles = new Vector3(0, 90, 0);
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            for (int i = 0; i <= spawnRelativeToDiff; i++)
            {
                int rand = Random.Range(0, enemySetups.Count);
                EnemySetup eSetup = enemySetups[rand];
                GameObject enemyToSpawn = Instantiate(eSetup.prefabs, new Vector3((other.transform.position.x + Random.Range(0, width)), eSetup.spawnHeight), rotation);
                //TODO modify stat to fit the current level
                enemyToSpawn.GetComponent<Obsticales>().UpdateStatByWeitgh(GameplayController.instance.difficultyLevel);

            }
        }
    }
    private void OnDestroy()
    {
        
    }

    public void OnNotify(string eventType, object parameter)
    {
        if(eventType == "DifficultyLevelUpNotify")
        {
            SpawnUp((int)parameter);
        }
    }
    private void OnDisable()
    {
        GameplayController.instance.UnregisterObserver(this);
    }
}
