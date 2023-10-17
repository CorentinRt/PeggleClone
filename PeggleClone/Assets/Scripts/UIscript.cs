using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    [SerializeField] TMP_Text _ballText;
    [SerializeField] TMP_Text _scoreText;

    public void UpdateBallText(int numberBalls)
    {
        _ballText.text = numberBalls.ToString();
    }

    public void UpdateScoreText(int newScore) 
    {
        _scoreText.text = newScore.ToString();
    }
}
