using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private Nuke nuke;
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private AudioSource deathSound;

    private float _score;

    private void OnEnable()
    {
        canvasHUD.SetActive(false);
        nuke.ActivateNukeEffect();
        enemySpawner.SetActive(false);
        _score = gameManager.GetScore();
        scoreText.text = $"{_score:0000}";
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        deathSound.Play();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game");
    }
}
