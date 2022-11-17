using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    [SerializeField] private float _speed = 5f;
    private Vector3 offset = new Vector3(0, 0.75f, 0);

    [Header("Laser Info")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    
    [SerializeField]
    private bool isTripleShotActive = false;
    private float _canFire = -1f;
    private SpawnManager sManager;

    public int _lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        sManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            FireLaser();
        }
    }

    void Movement()
    {
        float _leftBound = -11f;
        float _rightBound = 11f;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.87f ,0), transform.position.z);

        if(transform.position.x > _rightBound)
        {
            transform.position = new Vector3(_leftBound, transform.position.y, 0);
        }
        else if(transform.position.x < _leftBound)
        {
            transform.position = new Vector3(_rightBound, transform.position.y, 0);
        }
        
    }

    void FireLaser()
    {
        

        if (isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if(!isTripleShotActive)
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    public void Damage()
    {
       
        _lives--;

        if(_lives < 1)
        {
            Destroy(this.gameObject);
            sManager.CanSpawn();
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotDown());
    }
    public void SpeedPowerup()
    {
        _speed = 8.5f;
        StartCoroutine(SpeedDown());
    }
    IEnumerator SpeedDown()
    {
        yield return new WaitForSeconds(5f);
        _speed = 5f;
    }

    IEnumerator TripleShotDown()
    {
        yield return new WaitForSeconds(5f);

        isTripleShotActive = false;
    }
}
