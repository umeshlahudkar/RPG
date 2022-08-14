using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerController
{
    public PlayerModel playerModel { get; private set; }
    public PlayerView playerView { get; private set; }
    public EnemyView enemyView { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }
    public GameObject bow { get; private set; }
    public GameObject sword { get; private set; }
    public Transform weaponPosition { get; private set; }
    TextMeshProUGUI healthText;
    TextMeshProUGUI XPText;
    TextMeshProUGUI levelText;
    public CharacterType characterType = CharacterType.Player;

    public PlayerController(PlayerModel _playerModel, PlayerView _playerView, Vector3 spwanPosition)
    {
        playerModel = _playerModel;
        playerView = GameObject.Instantiate<PlayerView>(_playerView);

        playerView.SetPlayerController(this);
        playerModel.SetPlayerController(this);

        agent = playerView.GetNavmeshAgent();
        agent.Warp(spwanPosition);

        SubscribeEvent();
    }

    // subscribing to the event
    private void SubscribeEvent()
    {
        EventService.Instance.OnEnemyDead += UpdateXP;
        EventService.Instance.OnLevelUp += UpdateDisplayOnLevelUp;
    }

    // sets the references
    public void SetReferences(Animator animator, GameObject bow, GameObject sword, Transform weaponPosition,
                              TextMeshProUGUI healthText, TextMeshProUGUI XPtext, TextMeshProUGUI LevelText)
    {
        this.animator = animator;
        this.bow = bow;
        this.sword = sword;
        this.weaponPosition = weaponPosition;
        this.healthText = healthText;
        this.XPText = XPtext;
        this.levelText = LevelText;
    }

    public void EquipWeapon(WeaponType _weaponType)
    {
        DisarmedWeapon();
        playerModel.SetWeaponStat(_weaponType); // sets the weapon stats like animator and stoping distance to attack

        switch (_weaponType)
        {
            case WeaponType.Sword:
                sword.gameObject.SetActive(true);
                break;

            case WeaponType.Bow:
                bow.gameObject.SetActive(true);
                break;
        }

        DisplayInfo(_weaponType.ToString() + " Equipped");
        SoundManager.Instance.PlaySFXSound(SoundType.WeaponPickUp);
    }

    private void DisarmedWeapon()
    {
        switch(playerModel.weaponType)
        {
            case WeaponType.Bow :
                bow.gameObject.SetActive(false);
                break;

            case WeaponType.Sword:
                sword.gameObject.SetActive(false);
                break;
        }
    }

    public void OverrideAnimator()
    {
        if(playerModel.animatorOverrideController != null)
        {
            animator.runtimeAnimatorController = playerModel.animatorOverrideController;
        }
    }

    // cast the ray and based on hitInfo and user input, player will move or attack
    public void UserInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            enemyView = hitInfo.transform.GetComponent<EnemyView>();
            Vector3 targetPosition = hitInfo.point;
            if (enemyView != null)
            {
                if (!CanAttack() && Input.GetMouseButtonDown(1))
                {
                    MoveToward(enemyView.transform.position);
                }

                if (CanAttack())
                {
                    StopMoving();
                    Rotate();

                    if (Input.GetMouseButtonDown(1))
                        Attack();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    StopFightAnimation();
                    MoveToward(targetPosition);
                }
            }
        }
    }

    // Rotate the player toward the enemy
    private void Rotate()
    {
        Vector3 direction = enemyView.transform.position - playerView.transform.position;
        playerView.transform.rotation = Quaternion.LookRotation(direction * Time.deltaTime);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    private void MoveToward(Vector3 targetPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

    public bool CanAttack()
    {
        float distance = Vector3.Distance(enemyView.transform.position, playerView.transform.position);
        return (distance < playerModel.stopDistance);
    }

    // based on weapon equipped, player attacks
    public void Attack()
    {
        ResetStopFightAnimationTrigger();
        PlayFightAnimation();

        switch(playerModel.weaponType)
        {
            case WeaponType.Bow :
                WeaponService.Instance.CreateWeapon(playerModel.weaponType, CharacterType.Player, weaponPosition);
                SoundManager.Instance.PlaySFXSound(SoundType.ArrowLaunch);
                break;

            case WeaponType.FireBall:
                WeaponService.Instance.CreateWeapon(playerModel.weaponType, CharacterType.Player, weaponPosition);
                SoundManager.Instance.PlaySFXSound(SoundType.FireBallLaunch);
                break;

            case WeaponType.Sword:
                SoundManager.Instance.PlaySFXSound(SoundType.SwordHit);
                break;

            case WeaponType.Unarmed:
                SoundManager.Instance.PlaySFXSound(SoundType.MeleeHit);
                break;
        }
    }

    // plays the idle, walk or run animation based on player velocity on z-axis
    public void UpdateAnimatorSpeed()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = agent.transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("ForwardSpeed", speed);
    }

    // Playes the Attack Animation
    public void PlayFightAnimation()
    {
        animator.SetTrigger("attack");
    }

    // During playing Fight animation, if player Runs away stops the fight animation
    public void StopFightAnimation()
    {
        animator.SetTrigger("stopAttack");
    }

    // Reset the StopAttack trigger
    public void ResetStopFightAnimationTrigger()
    {
        animator.ResetTrigger("stopAttack");
    }

    // Update the player XP count and Display
    public void UpdateXP()
    {
        playerModel.XP += 10;
        playerModel.xpCount += 10;
        XPText.text = playerModel.XP.ToString();
        if(playerModel.xpCount >= playerModel.xpCountToUpdateLevel)
        {
            UpdateLevel();
            playerModel.xpCount = 0;
        }
    }

    // Update Level count
    public void UpdateLevel()
    {
        playerModel.level++;
        EventService.Instance.InvokeOnLevelUpEvent();
    }

    // Update level count on display and Increses the health to initail health
    public void UpdateDisplayOnLevelUp()
    {
        levelText.text = playerModel.level.ToString();
        DisplayInfo("Level UP");
        playerModel.SetHealth(playerModel.initialHealth);
        UpdateHealth(0, CharacterType.None);
        SoundManager.Instance.PlaySFXSound(SoundType.WeaponPickUp);
    }

    // Update the health after certain damage and Display health
    public void UpdateHealth(int damage, CharacterType characterType)
    {
        if (this.characterType == characterType) return;

        int health = playerModel.health - damage;
        playerModel.SetHealth(health);

        healthText.text = playerModel.health.ToString() + "/" + playerModel.initialHealth;

        if (playerModel.health <= 0)
        {
            animator.SetTrigger("die");
            UnsubscribeEvent();
            playerView.enabled = false;
            EventService.Instance.InvokeOnPlayerDiedEvent();
        }
    }

    // Display weapon wquipped and Level up text
    public async void DisplayInfo(string info)
    {
        playerView.display.SetActive(true);
        playerView.displayInfo.text = info;

        await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(3));

        playerView.display.SetActive(false);
    }

    private void UnsubscribeEvent()
    {
        EventService.Instance.OnEnemyDead -= UpdateXP;
        EventService.Instance.OnLevelUp -= UpdateDisplayOnLevelUp;
    }
}

