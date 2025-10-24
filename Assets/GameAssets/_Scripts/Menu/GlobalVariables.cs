using System;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    [SerializeField] private float dif;
    
    private static float _difficulty; //1.0 = normal, 1.5 = hard, 2.0 = nightmare

    private void Update()
    {
        dif = GetDifficulty();
    }

    public static void ChangeDifficulty(float valor)
    {
        print("Valor: " + valor);
        _difficulty = valor;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
    }

    public static float GetDifficulty()
    {
        return _difficulty;
    }
}
