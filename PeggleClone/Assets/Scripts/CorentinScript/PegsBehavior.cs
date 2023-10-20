using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PegsBehavior : MonoBehaviour
{
    // Fields
    [SerializeField] SpriteRenderer _sprite;

    [Header("Specificité")]
    [SerializeField] private bool _isImportant;
    private enum Specialties { NONE, GIVEBALL, SCORE, BOUNCE, POWER}
    [SerializeField] private Specialties _currentSpecialtie;
    [SerializeField] float BounceMultiplier;

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
    [SerializeField] private Sprite _importantSprite;

    [SerializeField] private Sprite _giveballSprite;
    [SerializeField] private Sprite _scoreSprite;
    [SerializeField] private Sprite _bounceSprite;
    [SerializeField] private Sprite _powerSprite;


    [Header("Gestion effets")]
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private ParticleSystem _explosionParticles2;
    private ParticleSystem _pointsParticles;
    [SerializeField] private ParticleSystem _pointsParticles1000;
    [SerializeField] private ParticleSystem _pointsParticles2000;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private float _initialPitch;
    [SerializeField] private float _pitchGap;

    [SerializeField] private Material _hitMaterial;

    private Sprite _currentSprite;

    [Header("Event")]
    [SerializeField] UnityEvent OnHit;
    // Properties


    // Methods
    private void Hit()
    {
        OnHit.Invoke();
        _hasBeenTouched = true;
        _sprite.material = _hitMaterial;
        switch (_currentSpecialtie)
        {
            case Specialties.POWER:

                _audioManager.PlayPowerReadySound();

                CanonScript.instance.powerAvailable = true;
                UIScript.instance.powerUpGauge.StartGaugeAnimation(true);
                break;
            case Specialties.SCORE:
                GameManager.Instance.AddPoints( 2 * _pointsValue);
                break;
            case Specialties.GIVEBALL:
                BallManager.instance.ballsRemaining++;
                UIScript.instance.UpdateBallText(BallManager.instance.ballsRemaining);
                break;
        }
    }

    private void DestructPeggle()
    {
        if (_hasBeenTouched)
        {
            // Explosions particles
            ParticleSystem currentExplosion = Instantiate(_explosionParticles, transform.position, Quaternion.identity);
            ParticleSystem currentExplosion2 = Instantiate(_explosionParticles2, transform.position, Quaternion.identity);
            ParticleSystem currentPoints = Instantiate(_pointsParticles, transform.position, Quaternion.identity);

            currentExplosion.Play();
            currentExplosion2.Play();
            currentPoints.Play();

            // Points and importantPegglesCount
            if(_isImportant)
            {
                _gameManager.NumberToDestroy--;
            }

            _gameManager.AddPoints(_pointsValue);

            // Reset Pitch audioSource
            _audioManager.GetComponent<AudioSource>().pitch = _initialPitch;

            // Destruction
            Destroy(gameObject);
        }
    }

    public void PeggleHit()
    {
        // Make the sound more high
        _audioManager.GetComponent<AudioSource>().pitch += _pitchGap;
        _audioManager.PlayHitSound();
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


        switch (_currentSpecialtie)
        {
            case Specialties.NONE:
                if (_isImportant)
                {
                    _gameManager.NumberToDestroy++;

                    _pointsValue = 2000;
                    _pointsParticles = _pointsParticles2000;

                    _currentSprite = _importantSprite;
                }
                else
                {
                    _pointsValue = 1000;
                    _pointsParticles = _pointsParticles1000;

                    _currentSprite = _normalSprite;
                }
                break;

            case Specialties.SCORE:
                _currentSprite = _scoreSprite;
                _pointsValue = 1000;
                _pointsParticles = _pointsParticles2000;

                break;
            case Specialties.GIVEBALL:
                _currentSprite = _giveballSprite;
                _pointsParticles = _pointsParticles1000;
                _pointsValue = 1000;
                break;
            case Specialties.BOUNCE:
                _currentSprite = _bounceSprite;
                _pointsValue = 1000;
                _pointsParticles = _pointsParticles1000;
                break;
            case Specialties.POWER:
                _currentSprite = _powerSprite;
                _pointsValue = 1000;
                _pointsParticles = _pointsParticles1000;
                break;
        }

        
        if (_currentSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = _currentSprite;
        }

        _gameManager = GameManager.Instance;

        // _audioManager = AudioManager.Instance;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ProxiTrigger" && !_hasBeenTouched) Hit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle") && !_hasBeenTouched)
        {
            Hit();
            if (_currentSpecialtie == Specialties.BOUNCE)
            {
                Rigidbody2D PlayerRB = collision.transform.GetComponent<Rigidbody2D>();
                PlayerRB.velocity = (new Vector2(-PlayerRB.velocity.x * BounceMultiplier,
                                                 -PlayerRB.velocity.y * BounceMultiplier));
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
