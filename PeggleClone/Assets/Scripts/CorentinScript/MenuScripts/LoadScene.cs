using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Fields

    private GameManager _gameManager;

    // Properties

    // Methods
    public void ChangeScene(string sceneName)
    {
        PlayerPrefs.SetInt("playerPoints", _gameManager.TotalPoints);

        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_gameManager == null)
        {
            _gameManager = GameManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
