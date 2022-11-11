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
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
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
        
        Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
    }
}
