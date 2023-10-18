using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    public delegate void NoBallsRemaining();
    public static event NoBallsRemaining OnNoBalls;

    [SerializeField] int _balls;

    public int ballsRemaining { get => _balls; set { 
            _balls = value;
            if(_balls == 0) OnNoBalls();
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
