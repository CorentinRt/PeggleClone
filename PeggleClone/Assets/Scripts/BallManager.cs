using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    public delegate void NoBallsRemaining(bool hasNo);
    public static event NoBallsRemaining OnNoBalls;

    [SerializeField] int _balls;

    public int ballsRemaining { get => _balls; set { 
            _balls = value;
            OnNoBalls(_balls == 0);
        } 
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
