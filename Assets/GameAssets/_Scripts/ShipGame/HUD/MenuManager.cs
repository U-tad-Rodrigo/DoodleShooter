using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject deathCanvas;
    
    private void Start()
    {
        hudCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        
        InputManager.Instance.PausePerformed += PauseOnPerformed;
        InputManager.Instance.UnPausePerformed += UnPauseOnPerformed;
    }

    public void UnPauseOnPerformed()
    {
        hudCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void PauseOnPerformed()
    {
        hudCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        deathCanvas.SetActive(false);
        Time.timeScale = 0.00001f;
    }

    public void ReturnToManu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }
}
