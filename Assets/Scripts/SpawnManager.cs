using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject _enemyPrefab;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] private GameObject[] _powerups;
    // Start is called before the first frame update

    public void Spawn()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-6.5f, 6.5f), 6f, 0);
           GameObject spawn = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            spawn.transform.parent = _enemyContainer.transform.parent;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 6f, 0);

            int randomPowerup = Random.Range(0, _powerups.Length);
            Instantiate(_powerups[randomPowerup], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 12));
        }
    }

    public void CanSpawn()
    {
        _stopSpawning = true;
        
    }


}
