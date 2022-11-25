using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private const float _powerupSpeed = 3f;

    private Player player;
    private AudioManager sManager;

    [SerializeField] private int powerupId;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed  *Time.deltaTime);

        if(transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);

            switch (powerupId)
            {
                case 0:
                player.TripleShotActive();
                break;

                case 1:
                    player.SpeedPowerup();
                break;

                case 2:
                    player.ShieldPowerup();
                    sManager._shieldAudio.Play();
                break;
                
                default:
                Debug.Log("Invalid");
                break;
            }
        }
    }
}
