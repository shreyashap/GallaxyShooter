using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private const float _powerupSpeed = 3f;

    private Player player;
    [SerializeField] private int powerupId;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
                    Debug.Log("Shield powerup activated");
                break;
                
                default:
                Debug.Log("Invalid");
                break;
            }
        }
    }
}
