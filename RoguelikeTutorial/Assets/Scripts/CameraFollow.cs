using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offset;

    private void Update ()
    {
        if (player == null) return;
        var desiredPosition = player.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing);
        transform.position = smoothedPosition;
    }
}
