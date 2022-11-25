using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _laserAudio;
    [SerializeField] private AudioSource _explosionAudio;
     public AudioSource _shieldAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireLaser()
    {
        _laserAudio.Play();
    }
    public void ExplosionSound()
    {
        _explosionAudio.Play();
    }
    
       
    
}
