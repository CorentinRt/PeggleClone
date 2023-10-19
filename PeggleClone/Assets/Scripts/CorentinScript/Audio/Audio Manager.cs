using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Fields

    private static AudioManager _instance;
    private AudioSource _source;

    [Header("Tous les sons du jeu")]
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _powerReady;
    [SerializeField] private AudioClip _powerShot;
    [SerializeField] private AudioClip _shotBall;
    [SerializeField] private AudioClip _lostBall;
    [SerializeField] private AudioClip _musicMenu;
    [SerializeField] private AudioClip _levelMenu;

    

    // Properties
    public static AudioManager Instance { get => _instance; set => _instance = value; }


    // Methods
    public void PlayHitSound()
    {
        _source.PlayOneShot(_hitClip);
    }
    public void PlayPowerReadySound()
    {
        _source.PlayOneShot(_powerReady);
    }
    public void PlayPowerShotSound()
    {
        _source.PlayOneShot(_powerShot);
    }
    public void PlayShotBallSound()
    {
        _source.PlayOneShot(_shotBall);
    }
    public void PlayLostBallSound()
    {
        _source.PlayOneShot(_lostBall);
    }

    private void Awake()
    {
        _instance = this;
        _source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
