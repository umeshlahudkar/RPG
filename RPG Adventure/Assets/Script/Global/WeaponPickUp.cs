using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] WeaponType weaponType;

    public WeaponType GetWeaponType()
    {
        Destroy(gameObject);
        return weaponType;
    }
}
