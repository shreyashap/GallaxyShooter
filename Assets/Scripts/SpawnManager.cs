using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private bool _stopSpawning = false;
    [SerializeField] private GameObject _enemyContainer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (_stopSpawning == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-9f, 9f), 6f, 0);
           GameObject spawn = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            spawn.transform.parent = _enemyContainer.transform.parent;
            yield return new WaitForSeconds(5f);
        }

      if(_enemyPrefab.transform.position.y < -6f)
        {
            Destroy(_enemyPrefab.gameObject);
        }
    }

    public void CanSpawn()
    {
        _stopSpawning = true;
        
    }


}
