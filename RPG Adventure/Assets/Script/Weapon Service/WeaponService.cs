using System;
using UnityEngine;

public class WeaponService : GenericSingleton<WeaponService>
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] FireBallPool fireBallPool;
    [SerializeField] ArrowPool arrowPool;

    public void CreateWeapon( WeaponType weaponType, CharacterType characterType, Transform position)
    {
        WeaponSO.WeaponStat weaponStat = GetWeaponSO(weaponType, characterType);

        switch(weaponType)
        {
            case WeaponType.Bow:
                ArrowModel arrowModel = new ArrowModel(weaponStat);
                ArrowController arrowController = arrowPool.GetItem(arrowModel, weaponStat.weaponPrefab, position, characterType);
                arrowController.Enable(position);
                break;

            case WeaponType.FireBall:
                FireBallModel fireBallModel = new FireBallModel(weaponStat);
                FireBallController fireBallController = fireBallPool.GetItem(fireBallModel, weaponStat.weaponPrefab, position, characterType);
                fireBallController.Enable(position);
                break;
        }
    }

    private WeaponSO.WeaponStat GetWeaponSO(WeaponType weaponType, CharacterType characterType)
    {
        WeaponSO.WeaponStat weaponStats = Array.Find(weaponSO.weapons, item => (item.weaponType == weaponType) && (item.characterType == characterType));
        if (weaponSO != null) return weaponStats;
        return null;
    }
}

public enum WeaponType
{
    None,
    Unarmed,
    Sword,
    Bow,
    FireBall
}

public enum CharacterType
{
    None,
    Player,
    Enemy
}