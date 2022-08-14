using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Object / Player")]
public class PlayerSO : ScriptableObject
{
    public PlayerView playerView;
    public int health;
    public int damagePoint;
    public EquipWeaponStat[] stat;

    [System.Serializable]
    public class EquipWeaponStat
    {
        public WeaponType weaponType;
        public AnimatorOverrideController overrideController;
        public float stoppingDistance;
    }
}
