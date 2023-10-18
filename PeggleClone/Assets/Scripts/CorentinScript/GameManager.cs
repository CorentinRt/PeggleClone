using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields

    private static GameManager _instance;

    [SerializeField] private LoadScene _scene;

    [Header("Win Condition")]
    [SerializeField] private int _numberToDestroy;
    private bool _hasWin;
    [SerializeField] private float _changeSceneCooldown;
    [Header("Following Scene")]
    [SerializeField] string _nextScene;

    [Header("Gestion points")]
    [SerializeField] private int _totalPoints;

    

    // Properties
    public int NumberToDestroy { get => _numberToDestroy; set => _numberToDestroy = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }


    // Methods
    private void Victory()
    {
        Debug.Log("You won !");
        
        StartCoroutine(WinCoroutine());
    }

    public void AddPoints(int points)
    {
        _totalPoints += points;
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
}
