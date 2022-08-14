using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float timeSinceLastAttack = Mathf.Infinity;
    public EnemyAttackState(EnemyController enemyController) : base(enemyController) { }
    public override void Enter()
    {
        base.Enter();
        enemyController.agent.isStopped = true;
    }

    public override void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        enemyController.enemyView.transform.LookAt(PlayerService.Instance.activePlayer.transform.position); // Face toward the player
        if (timeSinceLastAttack >= enemyController.enemyModel.timeBetweenAttack)
        {
            timeSinceLastAttack = 0;
            ResetTriggerFightAnimation();
            TriggerFightAnimation();
            Attack();
        }

        if (!CanAttack())
        {
            enemyController.ChangeState(State.Patrol); // changing state based on condition
        }

        base.Update();
    }

    // Creating weapon based on equipped weapon and plays the related audio clip
    private void Attack()
    {
        switch(enemyController.enemyModel.weaponType)
        {
            case WeaponType.Bow:
                WeaponService.Instance.CreateWeapon(WeaponType.Bow, CharacterType.Enemy ,enemyController.weaponPosition);
                SoundManager.Instance.PlaySFXSound(SoundType.ArrowLaunch);
                break;

            case WeaponType.FireBall:
                WeaponService.Instance.CreateWeapon(WeaponType.FireBall, CharacterType.Enemy, enemyController.weaponPosition);
                SoundManager.Instance.PlaySFXSound(SoundType.FireBallLaunch);
                break;

            case WeaponType.Unarmed:
                SoundManager.Instance.PlaySFXSound(SoundType.MeleeHit);
                break;

            case WeaponType.Sword:
                SoundManager.Instance.PlaySFXSound(SoundType.SwordHit);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopFightAnimation();
        enemyController.agent.isStopped = false;
    }
}
