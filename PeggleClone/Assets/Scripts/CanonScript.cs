using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CanonScript : MonoBehaviour
{
    public static CanonScript instance;

    [SerializeField] List<Sprite> _canonSprites;
    [SerializeField] List<Sprite> _decoPersoSprites;
    [SerializeField] List<Sprite> _portraitPersoSprites;

    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Transform _ballSpawningPoint;
    [SerializeField] InputActionReference _inputLaunch;
    [SerializeField] InputActionReference _inputPower;
    bool _canShoot = true;
    bool _isPlaying = true;
    Vector3 _currentDirection;

    [Header("Force")]
    [SerializeField] [Range(0,100f)]  float _horizontalForce;
    [SerializeField] [Range(0,100f)]  float _verticalForce;

    

    public enum PowerList { SIZE, FORCE, PROXI};
    [Header("Powers")]
    [SerializeField] PowerList _currentPower;
    [Space(3)]
    [SerializeField] float _powerSizeFactor;
    [SerializeField][Range(0, 100f)] float _powerHorizontalForce;
    [SerializeField][Range(0, 100f)] float _powerVerticalForce;
    bool _activePower = false;
    bool _powerAvailable = false;

    public PowerList currentPower { get => _currentPower; set => _currentPower = value; }
    public bool powerAvailable { get => _powerAvailable; set => _powerAvailable = value; }

    [SerializeField] UnityEvent _onFire;
    [SerializeField] UnityEvent _onPowerActivate;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Sprite _ultCharacter1;
    [SerializeField] private Sprite _ultCharacter2;
    [SerializeField] private Sprite _ultCharacter3;
    private Sprite _currentUltCharacter;

    private void Start()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        string _currentCharacter = "character1";
        if (PlayerPrefs.HasKey("character")) _currentCharacter = PlayerPrefs.GetString("character");
        SpriteRenderer _sR = GetComponentInChildren<SpriteRenderer>();
        switch (_currentCharacter)
        {
            case "character1":
                _sR.sprite = _canonSprites[0];
                UIScript.instance.decoPerso = _decoPersoSprites[0];
                UIScript.instance.portraitPerso = _portraitPersoSprites[0];

                _currentUltCharacter = _ultCharacter1;

                _currentPower = PowerList.FORCE;
                break;
            case "character2":
                _sR.sprite = _canonSprites[1];
                UIScript.instance.decoPerso = _decoPersoSprites[1];
                UIScript.instance.portraitPerso = _portraitPersoSprites[1];

                _currentUltCharacter = _ultCharacter2;

                _currentPower = PowerList.SIZE;
                break;
            case "character3":
                _sR.sprite = _canonSprites[2];
                UIScript.instance.decoPerso = _decoPersoSprites[2];
                UIScript.instance.portraitPerso = _portraitPersoSprites[2];

                _currentUltCharacter = _ultCharacter3;

                _currentPower = PowerList.PROXI;
                break;
        }
        UIScript.instance.UpdateBallText(BallManager.instance.ballsRemaining);
    }

    private void OnEnable()
    {
        BallScript.OnFallen += ResetCanShoot;
        BallManager.OnNoBalls += StopPlaying;
    }

    private void Update()
    {
        //projection de la balle
        bool launchInput = _inputLaunch.action.WasPressedThisFrame();
        if (launchInput  && _canShoot && _isPlaying)
        {
            _audioManager.PlayShotBallSound();

            _canShoot = false;
            _onFire.Invoke();
            BallManager.instance.ballsRemaining--;
            UIScript.instance.UpdateBallText(BallManager.instance.ballsRemaining);

            GameObject ball = Instantiate(_ballPrefab, _ballSpawningPoint.position, Quaternion.identity);
            if (_activePower)
            {
                ball.GetComponent<SpriteRenderer>().sprite = _currentUltCharacter;
            }
            ball.GetComponent<BallScript>().AudioManager = _audioManager;

            Rigidbody2D ballRB2D = ball.GetComponent<Rigidbody2D>();
            ballRB2D.velocity = new Vector2(_currentDirection.x * _horizontalForce, _currentDirection.y * _verticalForce);

            if (_activePower)
            {
                switch(_currentPower)
                {
                    case PowerList.SIZE:
                        ball.transform.localScale = new Vector2(ball.transform.localScale.x * _powerSizeFactor, ball.transform.localScale.y * _powerSizeFactor);
                        break;
                    case PowerList.FORCE:
                        ballRB2D.velocity = new Vector2(_currentDirection.x * _powerHorizontalForce, _currentDirection.y * _powerVerticalForce);
                        break;
                    case PowerList.PROXI:
                        ball.GetComponent<BallScript>().activateProxi = true;
                        break;
                }

                _audioManager.PlayPowerShotSound();

                powerAvailable = false;
            }

            _activePower = false;
        }

        //Activation du pouvoir
        bool powerInput = _inputPower.action.WasPressedThisFrame();
        if(powerInput && _isPlaying && !_activePower && powerAvailable)
        {
            _activePower = true;
            UIScript.instance.powerUpGauge.StartGaugeAnimation(false);
            _onPowerActivate.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (_isPlaying)
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Regarde curseur
            _currentDirection = cursorPosition - transform.position;
            _currentDirection.Normalize();
            float angle = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg;

            Quaternion playerRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 270f));
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, 1f);


            float currentroation = transform.eulerAngles.z;
            if (currentroation < 180f) transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentroation, 0f, 90f));
            else transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentroation, 270f, 360f));

        }
    }

    void ResetCanShoot() => _canShoot = true;
    void StopPlaying(bool noMore) => _isPlaying = !noMore;
}
