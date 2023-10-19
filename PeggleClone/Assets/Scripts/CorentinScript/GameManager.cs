using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Fields

    private static GameManager _instance;

    [SerializeField] private LoadScene _scene;

    [Header("Win Condition")]
    [SerializeField] private int _numberToDestroy;
    private bool _hasWin;
    [SerializeField] private float _changeSceneCooldown;

    [SerializeField] private ParticleSystem _winParticle1;
    [SerializeField] private ParticleSystem _winParticle2;
    [SerializeField] private UnityEvent _victoryEvent;

    [Header("Following Scene")]
    [SerializeField] string _nextScene;
    [SerializeField] string _currentScene;

    [Header("Lose Condition")]
    [SerializeField] private bool _noMoreBall;

    [Header("Gestion points")]
    [SerializeField] private int _totalPoints;

    

    // Properties
    public int NumberToDestroy { get => _numberToDestroy; set => _numberToDestroy = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }


    // Methods
    private void Victory()
    {
        Debug.Log("You won !");
        
        _victoryEvent.Invoke();

        StartCoroutine(WinCoroutine());
    }

    private void hasNoMoreBall()
    {
        _noMoreBall = true;
    }
    private void Lose()
    {
        if (_noMoreBall)
        {
            Debug.Log("You lost !");

            StartCoroutine(LoseCoroutine());
        }
    }

    public void AddPoints(int points)
    {
        _totalPoints += points;
    }
    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        BallManager.OnNoBalls += hasNoMoreBall;
        BallScript.OnFallen += Lose;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_numberToDestroy <= 0 && !_hasWin)
        {
            _hasWin = true;
            Victory();
        }
        
    }

    IEnumerator WinCoroutine()
    {
        
        yield return new WaitForSeconds(_changeSceneCooldown);

        _scene.ChangeScene(_nextScene);

        yield return null;
    }

    IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(_changeSceneCooldown);

        _scene.ChangeScene(_currentScene);

        yield return null;
    }
}
