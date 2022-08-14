using System;
using UnityEngine;

public class EnemyService : GenericSingleton<EnemyService>
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] public Enemy[] enemy;  // holding the Enemy patrolPath and enemy Scriptable object

    private void Start()
    {
        for(int i = 0; i < enemy.Length; i++)
        {
            CreateEnemy(enemy[i].enemySO, enemy[i].patrolPath);
        }
    }

    public void CreateEnemy(EnemySO enemySO, GameObject patrolpath)
    {
        EnemyModel enemyModel = new EnemyModel(enemySO);
        EnemyController enemyController = enemyPool.GetItem(enemyModel, enemySO, patrolpath, CharacterType.Enemy);
    }

    [Serializable]
    public class Enemy
    {
        public GameObject patrolPath;
        public EnemySO enemySO;
    }
}
