using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForwarder : MonoBehaviour
{
    private void Update()
    {
        transform.forward = (transform.position - Game.Camera.transform.position).normalized;
    }
}
