using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Fields
    [SerializeField] private GameObject _firstMenuPanel;
    [SerializeField] private GameObject _secondMenuPanel;

    [SerializeField] private LoadScene _loadScene;

    // Properties

    // Methods
    public void PressPlay()
    {
        _firstMenuPanel.SetActive(false);
        _secondMenuPanel.SetActive(true);
    }
    public void PressQuit()
    {
        _loadScene.QuitGame();
    }
    public void ChooseCharacter(string character)
    {
        // Faire choix des personnages
        PlayerPrefs.SetString("character", character);
        _loadScene.ChangeScene("GameScene");
    }

    void Start()
    {
        _firstMenuPanel.SetActive(true);
        _secondMenuPanel.SetActive(false);
    }

    void Update()
    {
        
    }
}
