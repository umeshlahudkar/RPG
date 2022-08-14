using UnityEngine;

public class EnemyPool : ServicePool<EnemyController>
{
    EnemyModel enemyModel;
    EnemySO enemySO;
    GameObject patrolPath;
    CharacterType characterType;

    // set the references for creating new enemycontroller if not found in the generic pool
    public EnemyController GetItem(EnemyModel _fireBallModel, EnemySO enemySO, GameObject patrolPath, CharacterType characterType)
    {
        this.enemyModel = _fireBallModel;
        this.enemySO = enemySO;
        this.patrolPath = patrolPath;
        this.characterType = characterType;

        return GetItem(characterType);
    }

    // creating the new enemyController
    protected override EnemyController CreateItem()
    {
        EnemyController enemyController = new EnemyController(enemyModel, enemySO, patrolPath, characterType);
        return enemyController;
    }
}
