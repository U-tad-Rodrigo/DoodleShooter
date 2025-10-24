using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour 
{
    //Variables
    public static InputManager Instance { get; private set; } //Si solo tiene un elemento se vuelve estatico el elem
    private TestInputActions _inputActions; //Se pone la "_" porque es un elem privado

    //Acciones puntuales
    public Action JumpPerformed, FirePerformed, PausePerformed, UnPausePerformed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _inputActions = new TestInputActions();
        _inputActions.Player.Enable(); //Activa el action map
    }

    private void Start()
    {
        _inputActions.Player.Fire.performed += FireOnPerformed; 
        _inputActions.UI.UnPause.performed += UnPauseOnPerformed; 
        _inputActions.Player.Pause.performed += PauseOnPerformed; 
    }
    
    private void FireOnPerformed(InputAction.CallbackContext obj)
    {
        FirePerformed?.Invoke(); 
    }

    private void UnPauseOnPerformed(InputAction.CallbackContext obj)
    {
        UnPausePerformed?.Invoke();
        SwitchUiToPlayer();
    }

    private void PauseOnPerformed(InputAction.CallbackContext obj)
    {
        PausePerformed?.Invoke();
        SwitchPlayerToUi();
    }

    public float GetHorizontalMovement()
    {
        return _inputActions.Player.HorizontalMove.ReadValue<float>();
    }
    
    public float GetVerticalMovement()
    {
        return _inputActions.Player.VerticalMove.ReadValue<float>();
    }

    public void SwitchUiToPlayer()
    {
        _inputActions.UI.Disable();
        _inputActions.Player.Enable();
    }

    public void SwitchPlayerToUi()
    {
        _inputActions.UI.Enable();
        _inputActions.Player.Disable();
    }
}
