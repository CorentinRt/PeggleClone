using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public delegate void Fallen();
    public static event Fallen OnFallen;

    [SerializeField] GameObject _proxiTrigger;

    [SerializeField] private AudioManager _audioManager;
    public bool activateProxi {  get => _proxiTrigger.activeSelf; set => _proxiTrigger.SetActive(value); }

    public AudioManager AudioManager { get => _audioManager; set => _audioManager = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "endTrigger")
        {
            _audioManager.PlayLostBallSound();
            OnFallen();
            Destroy(gameObject);
        }
    }
}
