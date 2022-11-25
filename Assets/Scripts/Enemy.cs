using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
 
    private Player player;
    private Animator anim;
    private AudioManager aManager;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        aManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();     
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        
        /*if(transform.position.y < -5.5f && player._lives > 1)
        {
            transform.position = new Vector3(0, 6f, 0);

        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "Player")
        {   
            player.Damage();


            anim.SetTrigger("OnDeath");
            _speed = 0;
            aManager.ExplosionSound();
            Destroy(this.gameObject, 2.4f);
          
            
        }
        
        if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            player.ScoreToAdd(10);

            anim.SetTrigger("OnDeath");
            _speed = 0;
            aManager.ExplosionSound();
            Destroy(this.gameObject,2.4f);
            
            
        }
    }
}
