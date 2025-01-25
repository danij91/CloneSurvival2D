using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform transform_player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        transform_player = GameManager.Instance.playerController.transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = transform_player.position + offset;
        transform.position = desiredPosition;
    }
}