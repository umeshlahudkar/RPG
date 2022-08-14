using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Object / Enemy")]
public class EnemySO : ScriptableObject
{
    public WeaponType weaponType;
    public EnemyView enemyView;
    public Material material;
    public AnimatorOverrideController animatorOverrideController;
    public float timeBetweenAttack;
    public float chaseSpeed;
    public float patrolSpeed;
    public float chaseDistance;
    public float attackDistance;
    public float waypointWaitTime;
    public int health;
    public int damagePoint;
}
