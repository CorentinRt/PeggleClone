using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Fields

    private static GameManager _instance;

    [SerializeField] private LoadScene _scene;

    [Header("Win Condition")]
    [SerializeField] private int _numberToDestroy;
    private bool _hasWin;
    [SerializeField] private float _changeSceneCooldown;

    [SerializeField] private UnityEvent _victoryEvent;
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private RectTransform _victoryTextCenterPosition;

    [Header("Following Scene")]
    [SerializeField] string _nextScene;

    [Header("Lose Condition")]
    [SerializeField] private bool _noMoreBall;
    [SerializeField] private UnityEvent _loseEvent;
    [SerializeField] private TextMeshProUGUI _loseText;
    [SerializeField] private RectTransform _loseTextCenterPosition;
    [SerializeField] private Image _losePanel;

    [SerializeField] bool _hasLose; // Only use for debug

    [Header("Gestion points")]
    [SerializeField] private int _totalPoints;

    

    // Properties
    public int NumberToDestroy { get => _numberToDestroy; set => _numberToDestroy = value; }
    public static GameManager Instance { get => _instance; set => _instance = value; }
    public int TotalPoints { get => _totalPoints; set => _totalPoints = value; }


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
        UIScript.instance.UpdateScoreText(_totalPoints);
    }
    private void Awake()
    {
        _totalPoints = PlayerPrefs.GetInt("playerPoints");
        _instance = this;
    }

    private void OnEnable()
    {
        BallManager.OnNoBalls += hasNoMoreBall;
        BallScript.OnFallen += Lose;
    }

    private void OnDisable()
    {
        BallManager.OnNoBalls -= hasNoMoreBall;
        BallScript.OnFallen -= Lose;
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

        if(_hasLose)    // Only use for debug
        {
            _hasLose = false;
            Lose();
        }
    }

    IEnumerator WinCoroutine()
    {
        float currentTimer = 0;
        while(currentTimer < _changeSceneCooldown)
        {
            currentTimer += Time.deltaTime;

            float currentYPosition = _victoryText.rectTransform.position.y;

            currentYPosition = Mathf.Lerp(currentYPosition, _victoryTextCenterPosition.transform.position.y, Time.deltaTime);

            _victoryText.rectTransform.position = new Vector3(_victoryText.rectTransform.position.x, currentYPosition, _victoryText.rectTransform.position.z);

            yield return null;
        }

        _scene.ChangeScene(_nextScene);

        yield return null;
    }

    IEnumerator LoseCoroutine()
    {
        float currentTimer = 0;
        while (currentTimer < _changeSceneCooldown)
        {
            currentTimer += Time.deltaTime;

            float currentYPosition = _loseText.rectTransform.position.y;

            currentYPosition = Mathf.Lerp(currentYPosition, _loseTextCenterPosition.transform.position.y, Time.deltaTime);

            _loseText.rectTransform.position = new Vector3(_loseText.rectTransform.position.x, currentYPosition, _loseText.rectTransform.position.z);

            Color color = _losePanel.color;

            color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime);
            Debug.Log(color.a);

            _losePanel.color = color;

            yield return null;
        }

        _scene.ChangeScene(SceneManager.GetActiveScene().name);

        yield return null;
    }
}
