using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PegsBehavior : MonoBehaviour
{
    // Fields
    [Header("Specificité")]
    [SerializeField] private bool _isImportant;
    [SerializeField] private bool _isPowerUp;

    [Header("Gestion collision")]
    [SerializeField] private bool _hasBeenTouched;

    [SerializeField] private float _preDestructionTime;
    private float _currentPreDestructionTime;

    [Header("Gestion victoire")]
    [SerializeField] private GameManager _gameManager;

    [Header("Points")]
    [SerializeField] private int _pointsValue;

    [Header("Gestion Visual")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _normalSpriteTouched;
    [SerializeField] private Sprite _importantSprite;
    [SerializeField] private Sprite _importantSpriteTouched;

    [Header("Gestion effets")]
    [SerializeField] private ParticleSystem _explosionParticles;

    private Sprite _currentSprite;
    private Sprite _currentSpriteTouched;

    [Header("Event")]
    [SerializeField] UnityEvent OnHit;
    // Properties


    // Methods

    private void DestructPeggle()
    {
        if (_hasBeenTouched)
        {
            ParticleSystem currentExplosion = Instantiate(_explosionParticles, transform.position, Quaternion.identity);

            currentExplosion.Play();

            _gameManager.NumberToDestroy--;

            _gameManager.AddPoints(_pointsValue);

            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        BallScript.OnFallen += DestructPeggle;

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
            _pointsValue = 2000;

            _currentSprite = _importantSprite;
            _currentSpriteTouched = _importantSpriteTouched;
        }
        else
        {
            _pointsValue = 1000;

            _currentSprite = _normalSprite;
            _currentSpriteTouched = _normalSpriteTouched;
        }

        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle") && !_hasBeenTouched)
        {
            OnHit.Invoke();
            _hasBeenTouched = true;
            if (_isPowerUp)
            {
                CanonScript.instance.powerAvailable = true;
            }
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
