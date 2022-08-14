using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerView : MonoBehaviour, IDamageable
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject bow;
    private PlayerController playerController;
    [SerializeField] Transform weaponPosition;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI XPText;
    [SerializeField] TextMeshProUGUI levelText;
    public GameObject display;
    public TextMeshProUGUI displayInfo;

    private void Start()
    {
        playerController.SetReferences(animator, bow, sword, weaponPosition, healthText, XPText, levelText);
        playerController.EquipWeapon(WeaponType.Unarmed);
        playerController.UpdateHealth(0, CharacterType.None);
    }

    void Update()
    {
        playerController.UserInput();

        playerController.UpdateAnimatorSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponPickUp weapon = other.gameObject.GetComponent<WeaponPickUp>();
        if (weapon != null)
        {
            playerController.EquipWeapon(weapon.GetWeaponType());
        }

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(playerController.playerModel.damagePoint, playerController.characterType);
        }
    }

    public NavMeshAgent GetNavmeshAgent()
    {
        return navMeshAgent;
    }

    internal void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    void Hit() { }
    void Shoot() { }

    public void TakeDamage(int damage, CharacterType characterType)
    {
        playerController.UpdateHealth(damage, characterType);
        SoundManager.Instance.PlaySFXSound(SoundType.TakeDamage);
    }

}

