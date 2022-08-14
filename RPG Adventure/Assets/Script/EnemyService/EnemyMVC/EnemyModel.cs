using UnityEngine;

public class EnemyModel 
{
    public EnemyController enemyController { get; private set; }
    public AnimatorOverrideController animatorOverrideController { get; private set; }
    public float timeBetweenAttack { get; private set; }
    public float chaseSpeed { get; private set; }
    public float patrolSpeed { get; private set; }
    public float chaseDistance { get; private set; }
    public float attackDistance { get; private set; }
    public float wayPointsWaitTime { get; private set; }
    public int health { get; private set; }
    public int damagePoint { get; private set; }
    public WeaponType weaponType { get; private set; }

    public EnemyModel(EnemySO enemySO)
    {
        animatorOverrideController = enemySO.animatorOverrideController;
        timeBetweenAttack = enemySO.timeBetweenAttack;
        chaseSpeed = enemySO.chaseSpeed;
        patrolSpeed = enemySO.patrolSpeed;
        chaseDistance = enemySO.chaseDistance;
        attackDistance = enemySO.attackDistance;
        wayPointsWaitTime = enemySO.waypointWaitTime;
        health = enemySO.health;
        damagePoint = enemySO.damagePoint;
        weaponType = enemySO.weaponType;
    }

    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    internal void SetHealth(int value)
    {
        health = Mathf.Max(0, value);
    }
}
