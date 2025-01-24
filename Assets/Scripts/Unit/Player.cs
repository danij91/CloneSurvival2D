using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Unit
{
    public Weapon defaultWeaponPrefab;
    private PlayerController _playerController;

    public List<Weapon> Weapons { get; set; }

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
        Weapons = new();
        Weapon defaultWeapon = Instantiate(defaultWeaponPrefab, transform);

        Weapons.Add(defaultWeapon);
        _playerController = GameManager.Instance.playerController;
    }
}