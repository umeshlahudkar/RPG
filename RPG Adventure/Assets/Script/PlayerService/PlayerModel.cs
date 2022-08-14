using System;
using UnityEngine;

public class PlayerModel
{
    public PlayerController playerController;

    public int health { get; private set; }
    public int damagePoint { get; private set; }
    public PlayerSO playerSO { get; private set; }

    public float stopDistance { get; private set; }
    public AnimatorOverrideController animatorOverrideController { get; private set; }
    public WeaponType weaponType { get; private set; }
    public int XP { get; set; }
    public int level { get; set; }
    public int initialHealth { get; private set; }
    public int xpCountToUpdateLevel { get; set; }
    public int xpCount { get; set; }

    public PlayerModel(PlayerSO playerSO)
    {
        health = initialHealth = playerSO.health;
        damagePoint = playerSO.damagePoint;
        xpCountToUpdateLevel = 40;
        this.playerSO = playerSO;
    }

    // sets the animator and stopping disance based on weapon equipped
    public void SetWeaponStat(WeaponType _weaponType)
    {
        PlayerSO.EquipWeaponStat stats = new PlayerSO.EquipWeaponStat();
        this.weaponType = _weaponType;
        stats = Array.Find(playerSO.stat, item => item.weaponType == _weaponType);
        animatorOverrideController = stats.overrideController;
        stopDistance = stats.stoppingDistance;

        if(animatorOverrideController != null)
        {
            playerController.OverrideAnimator();
        }
    }

    public void SetHealth(int value)
    {
        health = Mathf.Max(0, value);
    }

    internal void SetPlayerController(PlayerController _playerController)
    {
        playerController = _playerController;
    }
}
