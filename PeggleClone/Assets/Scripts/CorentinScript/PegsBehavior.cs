using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegsBehavior : MonoBehaviour
{
    // Fields
    [Header("Specificité")]
    private bool _isImportant;

    [Header("Gestion collision")]
    [SerializeField] private bool _hasBeenTouched;

    [SerializeField] private float _preDestructionTime;
    private float _currentPreDestructionTime;

    [Header("Gestion victoire")]
    [SerializeField] private GameManager _gameManager;

    [Header("Gestion Visual")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _normalSpriteTouched;
    [SerializeField] private Sprite _importantSprite;
    [SerializeField] private Sprite _importantSpriteTouched;

    private Sprite _currentSprite;
    private Sprite _currentSpriteTouched;

    // Properties


    // Methods

    private void DestructPeggle()
    {
        _gameManager.NumberToDestroy--;

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        if( _hasBeenTouched)
        {
            BallScript.OnFallen += DestructPeggle;
        }
    }

    private void OnDisable()
    {
        BallScript.OnFallen -= DestructPeggle;
    }

    // Start is called before the first frame update
    void Start()
    {
        _hasBeenTouched = false;
        if (_isImportant)
        {
            _currentSprite = _importantSprite;
            _currentSpriteTouched = _importantSpriteTouched;
        }
        else
        {
            _currentSprite = _normalSprite;
            _currentSpriteTouched = _normalSpriteTouched;
        }

        _gameManager = GameManager.Instance;
        Debug.Log(_gameManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle") && !_hasBeenTouched)
        {
            _hasBeenTouched = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Balle") && _hasBeenTouched)
        {
            _currentPreDestructionTime += Time.deltaTime;
            
            if(_currentPreDestructionTime >= _preDestructionTime)
            {
                DestructPeggle();
            }
        }
    }

}
