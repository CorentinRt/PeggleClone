using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields

    private static GameManager _instance;

    [Header("Win Condition")]
    [SerializeField] private int _numberToDestroy;
    private bool _hasWin;

    [Header("Gestion points")]
    [SerializeField] private int _totalPoints;

    // Properties
    public int NumberToDestroy { get => _numberToDestroy; set => _numberToDestroy = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }


    // Methods
    private void Victory()
    {
        Debug.Log("You won !");
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
            Victory();
        }
    }
}
