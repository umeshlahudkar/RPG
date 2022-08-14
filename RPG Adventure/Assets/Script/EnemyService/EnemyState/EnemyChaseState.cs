
public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
        base.Enter();
        enemyController.agent.speed = enemyController.enemyModel.chaseSpeed;
    }

    public override void Update()
    {
        MoveTowards(PlayerService.Instance.activePlayer.transform.position);
        base.Update();

        if (!CanChase()) 
        {
            enemyController.ChangeState(State.Idle); // changing state based on condition
        }

        if(CanAttack())
        {
            enemyController.ChangeState(State.Attack); // changing state based on condition
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyController.agent.speed = enemyController.enemyModel.chaseSpeed;
    }
}
