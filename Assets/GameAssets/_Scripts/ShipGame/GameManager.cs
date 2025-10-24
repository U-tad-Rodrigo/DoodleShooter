using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] private float score;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null) //si no existe la instancia asigna esta como ese
            Instance = this;
        else //si ya existe se elimine un duplicado 
            Destroy(gameObject);
        
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateScore(score);
        //StartCoroutine(AddScoreOverTime());  // Llama a la corrutina para a�adir experiencia cada 3 segundos
    }

    public void AddScore(float newScore)
    {
        this.score += newScore;
        UpdateScore(this.score);
    }
    public float GetScore()
    {
        return score;
    }

    private void UpdateScore(float newScore)
    {
        scoreText.text = $"Score: {newScore:0000}"; //cambio de formato de 0 a 0000 (0001, 0002, 0003, etc)
    }
}
