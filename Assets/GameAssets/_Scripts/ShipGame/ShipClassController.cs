using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipClassController : MonoBehaviour
{
    [Header("Player Ships")]
    [SerializeField] private GameObject playerNormal; // Objeto del jugador normal
    [SerializeField] private GameObject playerDoubleWeapon; // Objeto del jugador con doble arma
    [SerializeField] private GameObject playerShield; // Objeto del jugador con escudo
    
    [Header("Player Spawners")]
    [SerializeField] private GameObject spawnerPositionRegular; // Punto de spawn regular
    [SerializeField] private GameObject spawnerPositionShield; // Punto de spawn con escudo
    [SerializeField] private GameObject spawnerPositionDouble; // Primer punto de spawn para doble arma
    [SerializeField] private GameObject spawnerPositionDouble2; // Segundo punto de spawn para doble arma

    [Header("Player Arms")]
    [SerializeField] private GameObject playerArmsPositionRegular;
    [SerializeField] private GameObject playerArmsPositionShield;
    [SerializeField] private GameObject playerArmsPositionDouble;
    
    [Header("Player Orbs")]
    [SerializeField] private GameObject[] playerOrbs;
    
    private int _i = 0;
    private int _currentHealth; // Salud actual para almacenar antes de cambiar de modo
    private HealthGlobal _healthGlobal;
    private ProjectileSpawner _projectileSpawner;
    private bool _isShielded = false;
    private bool _isDouble = false;

    private void Start()
    {
        _projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
        _healthGlobal = GetComponent<HealthGlobal>();
        ActivatePlayerNormal();  // Asegura que el jugador comience en estado normal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Al recibir el impacto del enemgio/proyectil vuelve al jugador normal
        if ((other.CompareTag("Enemy") || other.CompareTag("ProjectileEn")) && GetIsDouble() == true)
        {
            SwitchMode();
            return; //Asegura que se sale y no se hace nada mas en este codigo
        }
        
        //Vida del shield
        if ((other.CompareTag("Enemy") || other.CompareTag("ProjectileEn")) && GetIsShielded() == true)
        {
            playerOrbs[_i].SetActive(false);
            
            if (_i >= 2)
            {
                SwitchMode();
                _i = 0;
                return; //Asegura que se sale y no se hace nada mas en este codigo}

            }
            
            _i++;
        }
    }

    // Metodo para activar el estado normal del jugador
    private void ActivatePlayerNormal()
    {
        //Activacion de nave
        playerNormal.SetActive(true);
        playerDoubleWeapon.SetActive(false);
        playerShield.SetActive(false);
        
        //activacion de brazos
        playerArmsPositionRegular.SetActive(true);
        playerArmsPositionShield.SetActive(false);
        playerArmsPositionDouble.SetActive(false);

        _healthGlobal.SetMaxHealth();  // Asegura que la salud se restablezca a la m�xima al volver al modo normal

        // Restaurar el punto de spawn al regular
        List<Transform> regularSpawnPoint = new List<Transform>
        {
            spawnerPositionRegular.transform
        };
        _projectileSpawner.SetSpawnPoints(regularSpawnPoint);
    }

    // Metodo para activar el estado con doble arma
    public void DoubleWeapon()
    {
        _isDouble = true;
        
        //Activacion de nave
        playerNormal.SetActive(false);
        playerDoubleWeapon.SetActive(true);
        playerShield.SetActive(false);
        
        //activacion de brazos
        playerArmsPositionRegular.SetActive(false);
        playerArmsPositionShield.SetActive(false);
        playerArmsPositionDouble.SetActive(true);

        // Configura los puntos de spawn para disparos dobles
        List<Transform> doubleWeaponSpawnPoints = new List<Transform>
        {
            spawnerPositionDouble.transform,
            spawnerPositionDouble2.transform
        };
        _projectileSpawner.SetSpawnPoints(doubleWeaponSpawnPoints);
    }

    // Metodo para activar el estado con escudo
    public void Shield()
    {
        _isShielded = true;
        
        foreach (GameObject temp in playerOrbs)
        {
            temp.SetActive(true);
        }
        
        //Activacion de nave
        playerNormal.SetActive(false);
        playerDoubleWeapon.SetActive(false);
        playerShield.SetActive(true);
        
        //activacion de brazos
        playerArmsPositionRegular.SetActive(false);
        playerArmsPositionShield.SetActive(true);
        playerArmsPositionDouble.SetActive(false);

        // Configura el punto de spawn para el escudo
        List<Transform> shieldSpawnPoint = new List<Transform>
        {
            spawnerPositionShield.transform
        };
        
        _projectileSpawner.SetSpawnPoints(shieldSpawnPoint);
    }

    // Metodo para cambiar de modo o restaurar a normal cuando se muere
    public void SwitchMode()
    {
        if (_isDouble || _isShielded)
        {
            _isShielded = false;
            _isDouble = false;
            
            Debug.Log("Returning to normal mode due to death.");
            ActivatePlayerNormal(); // Si el jugador muere con un power-up, regresa al modo normal
            _healthGlobal.SetMaxHealth(); // Restaura la salud anterior
        }
    }

    public bool GetIsShielded()
    {
        return _isShielded;
    }
    
    public bool GetIsDouble()
    {
        return _isDouble;
    }
}
