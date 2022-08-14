using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Object / Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponStat[] weapons; 

    [System.Serializable]
    public class WeaponStat
    {
        public int weaponDamage;
        public int speed;
        public GameObject weaponPrefab;
        public CharacterType characterType;
        public WeaponType weaponType;
        public int timeToDisable;
    }
}
