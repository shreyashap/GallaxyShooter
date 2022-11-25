using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotSpeed;
    [SerializeField] private GameObject _explodePrefab;

    private SpawnManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 5 * _rotSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser")
        {
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(other.gameObject);

            manager.Spawn();
        }
    }
}
