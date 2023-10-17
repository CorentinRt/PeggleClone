using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Fields

    [Header("Win Concdition")]
    [SerializeField] private int _numberToDestroy;
    private bool _hasWin;

    // Properties
    public int NumberToDestroy { get => _numberToDestroy; set => _numberToDestroy = value; }


    // Methods
    private void Victory()
    {
        Debug.Log("You won !");
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
