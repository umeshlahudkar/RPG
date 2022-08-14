using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowModel : MonoBehaviour
{
    public ArrowController arrowController { get; private set; }
    public int weaponDamage { get; private set; }
    public int speed { get; private set; }
    public int timeToDisable { get; private set; }

    public ArrowModel(WeaponSO.WeaponStat weaponSO)
    {
        weaponDamage = weaponSO.weaponDamage;
        speed = weaponSO.speed;
        timeToDisable = weaponSO.timeToDisable;
    }

    public void SetArrowController(ArrowController arrowController)
    {
        this.arrowController = arrowController;
    }

}
