using System;
using UnityEngine;

public class Player : Unit
{
    private PlayerController _playerController;

    private void Start()
    {
        Initialize();
    }

    protected override void Die()
    {
        _playerController.enabled = false;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _playerController = GameManager.Instance.playerController;
    }
}