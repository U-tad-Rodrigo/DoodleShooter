using System.Collections;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] objectsToSpawn;  // Los cuatro objetos que se pueden spawnear
    [SerializeField] private float spawnInterval = 3f;      // Tiempo en segundos entre cada spawn
    [SerializeField] private float spawnAreaWidth = 5f;     // Ancho del �rea de spawn (en X)
    [SerializeField] private float spawnAreaHeight = 5f;    // Alto del �rea de spawn (en Y)

    [Header("Spawn Chances")]
    [SerializeField] private float chanceObject1 = 0.4f;  // Probabilidad de que spawnee el primer objeto
    [SerializeField] private float chanceObject2 = 0.3f;  // Probabilidad de que spawnee el segundo objeto
    [SerializeField] private float chanceObject3 = 0.2f;  // Probabilidad de que spawnee el tercer objeto
    [SerializeField] private float chanceObject4 = 0.1f;  // Probabilidad de que spawnee el cuarto objeto

    [Header("Score Settings")]
    [SerializeField] private float scoreThreshold = 100f;  // Valor de score para reducir el intervalo
    [SerializeField] private float reductionPercentage = 0.1f; // Porcentaje de reducci�n (10%)

    private float _currentScore = 0f;  // Almacena el score actual
    private int _roundCount = 1;  // Contador de rondas
    private float _percentageMod = 0.05f;
    private int _maxRounds = 10;
    private int _currentRounds = 0;

    void Start()
    {
        // Comienza el proceso de spawnear objetos cada intervalo de tiempo
        StartCoroutine(SpawnObjects());
    }

    void Update()
    {
        // Obtener el score actual desde el GameManager
        _currentScore = GameManager.Instance.GetScore();

        // Verifica si el score ha superado el umbral para reducir el intervalo
        if (_currentScore >= scoreThreshold && _currentRounds <= _maxRounds)
        {
            _currentRounds++;
            ReduceSpawnInterval();
            scoreThreshold += 0 + (_currentRounds * 1000);  // Duplica el umbral para la proxima reduccion
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Espera el tiempo especificado antes de spawnear
            yield return new WaitForSeconds(spawnInterval);

            // Elige el objeto para spawnear basado en las probabilidades
            GameObject objectToSpawn = GetRandomObject();

            // Genera una posici�n aleatoria dentro del �rea del spawner, en relaci�n a la posici�n del objeto que tiene el script
            Vector2 randomPosition = new Vector2(
                transform.position.x + Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
                transform.position.y + Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
            );

            // Spawnea el objeto en la posici�n aleatoria
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private GameObject GetRandomObject()
    {
        float randomValue = Random.value;

        // Compara el valor aleatorio con las probabilidades de cada objeto
        if (randomValue < chanceObject1)
        {
            return objectsToSpawn[0]; // Objeto 1
        }
        else if (randomValue < chanceObject1 + chanceObject2)
        {
            return objectsToSpawn[1]; // Objeto 2
        }
        else if (randomValue < chanceObject1 + chanceObject2 + chanceObject3)
        {
            return objectsToSpawn[2]; // Objeto 3
        }
        else
        {
            return objectsToSpawn[3]; // Objeto 4
        }
    }

    private void ReduceSpawnInterval()
    {
        spawnInterval -= spawnInterval * (reductionPercentage + _percentageMod);  // Reduce el intervalo de spawn
        if (_percentageMod > 0.15f)
            _percentageMod += 0.05f;
        _roundCount++;  // Incrementa la ronda
        Debug.Log($"Ronda {_roundCount}: Spawn interval reducido a {spawnInterval:F2} segundos");
    }
}
