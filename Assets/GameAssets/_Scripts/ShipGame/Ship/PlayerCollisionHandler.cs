using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ITriggerEnter>()?.HitByPlayer(gameObject);
    }
}
