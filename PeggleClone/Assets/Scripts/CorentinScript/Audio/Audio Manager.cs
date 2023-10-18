using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Fields

    private static AudioManager _instance;

    [SerializeField] private AudioClip _hitClip;

    // Properties
    public static AudioManager Instance { get => _instance; set => _instance = value; }


    // Methods
    public void PlayHitSound()
    {
        Debug.Log("hit sound");
    }

    private void Awake()
    {
        _instance = this;
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
