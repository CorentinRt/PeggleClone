using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    [SerializeField] Text _ballText;
    [SerializeField] Text _scoreText;

    public void UpdateBallText(int numberBalls)
    {
        _ballText.text = numberBalls.ToString();
    }

    public void UpdateScoreText(int newScore) 
    {
        _scoreText.text = newScore.ToString();
    }
}
