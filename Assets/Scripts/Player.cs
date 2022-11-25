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
    
    
   
    private float _canFire = -1f;
    private SpawnManager sManager;

    public int _lives = 3;
    
    private bool isTripleShotActive = false;
    private bool isSheildActive = false;


    [SerializeField] private int _score;
    private UIManager uiManager;
    private GameManager gManager;

    [Header("Effects Info")]
    [SerializeField] private GameObject _sheildVisualiser;
    [SerializeField] GameObject playerHurt1;
    [SerializeField] GameObject playerHurt2;

    private Animator playerAnim;
    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
       
        sManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


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


        if(horizontalInput > 0)
        {
            playerAnim.SetFloat("IsTurn",horizontalInput);
        }
        else if(horizontalInput < 0)
        {
            playerAnim.SetFloat("IsTurn", horizontalInput);
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

        audioManager.FireLaser();
    }

    public void Damage()
    {
       if(isSheildActive)
        {
            isSheildActive = false;
            _sheildVisualiser.SetActive(false);
            audioManager._shieldAudio.Stop();
            return;
        }
        
        _lives--;
        
        if(_lives == 2)
        {
            playerHurt1.SetActive(true);
        }
        else if(_lives == 1)
        {
            playerHurt2.SetActive(true);
        }
        

        uiManager.LivesImage(_lives);
      

        if(_lives < 1)
        {
           
            Destroy(this.gameObject);
            sManager.CanSpawn();
            uiManager.GameOver();
            gManager.GameRestart();
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

    public void ShieldPowerup()
    {
        isSheildActive = true;
        _sheildVisualiser.SetActive(true);
        
    }

    public void ScoreToAdd(int addScore)
    {
        _score = _score + addScore;

        uiManager.UpdateScore(_score);
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
