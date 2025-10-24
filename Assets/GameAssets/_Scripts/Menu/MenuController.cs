using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void PlayClicked()
    {
        print("Play clicked.");
        SceneManager.LoadScene("Game");
    }

    public void OptionsClicked()
    {
        print("Options clicked.");
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackClicked()
    {
        print("Back clicked.");
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    
    public void QuitClicked()
    {
        print("Quit clicked.");
        Application.Quit();
    }
}
