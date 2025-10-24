using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationTime;

    private Rigidbody2D _rb;

    // Limites de la pantalla
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // Obtener los limites de la camara
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y;
    }

    private void Update()
    {
        _rb.velocity = Movementlogic();

        if (_rb.velocity.x > 0.5f)
        {
            transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45f, -20f), rotationTime);
        }
        else if (_rb.velocity.x < -0.5f)
        {
            transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45f, 20f), rotationTime);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationTime);
        }

        // Limitar la posici�n de la nave dentro de los l�mites de la pantalla
        ClampPosition();
    }

    private Vector2 Movementlogic()
    {
        var horizontal = InputManager.Instance.GetHorizontalMovement();
        var vertical = InputManager.Instance.GetVerticalMovement();

        return (Vector2.right * horizontal + Vector2.up * vertical).normalized * (speed * Time.deltaTime); // Coge los vectores horizontales y verticales y los hace un vector
    }

    private void ClampPosition()
    {
        // Limitar la posici�n de la nave para que no se salga de la pantalla
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Asignar la posici�n limitada
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
