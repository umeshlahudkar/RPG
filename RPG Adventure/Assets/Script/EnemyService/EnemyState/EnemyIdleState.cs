using UnityEngine;

public class EnemyIdleState : EnemyState
{
    float idleHaltTime = 3f;
    float currentTime = 0;
    public EnemyIdleState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
        base.Enter();
        enemyController.agent.isStopped = true;
    }

    // after certain time changing state from idle to petroling
    public override void Update()
    {
        base.Update();
        currentTime += Time.deltaTime;
        if(currentTime >= idleHaltTime)
        {
            currentTime = 0;
            enemyController.ChangeState(State.Patrol);
        }

    }

    public override void Exit()
    {
        base.Exit();
        enemyController.agent.isStopped = false;
    }
}
