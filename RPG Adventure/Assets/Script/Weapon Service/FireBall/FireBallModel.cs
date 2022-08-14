using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallModel 
{
    public FireBallController fireBallController { get; private set; }
    public int weaponDamage { get; private set; }
    public int speed { get; private set; }
    public int timeToDisable { get; private set; }
    public FireBallModel(WeaponSO.WeaponStat weaponSO)
    {
        weaponDamage = weaponSO.weaponDamage;
        speed = weaponSO.speed;
        timeToDisable = weaponSO.timeToDisable;
    }

    public void SetFireController(FireBallController fireBallController)
    {
        this.fireBallController = fireBallController;
    }
}
