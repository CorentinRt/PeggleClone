using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;

    [SerializeField] TMP_Text _ballText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] UIPowerUpGauge _powerUpGauge;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject); 
        instance = this;
    }

    public void UpdateBallText(int numberBalls)
    {
        _ballText.text = numberBalls.ToString();
    }

    public void UpdateScoreText(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
