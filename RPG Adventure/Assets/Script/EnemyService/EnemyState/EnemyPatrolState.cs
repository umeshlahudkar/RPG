using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    int index = 0;
    float timeSinceWaypointReached = 0;
    public EnemyPatrolState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter()
    {
        base.Enter();
        enemyController.agent.speed = enemyController.enemyModel.patrolSpeed; //sets speed of navmesh agent while patroling
    }

    public override void Update()
    {
        MoveTowards(enemyController.patrolPath.transform.GetChild(index).position);

        // Checks if the distance between enemy and waypoints less than one, changes the waypoint for the movement
        if (Vector3.Distance(enemyController.enemyView.transform.position, enemyController.patrolPath.transform.GetChild(index).position) <= 1)
        {
            timeSinceWaypointReached += Time.deltaTime;

            if (timeSinceWaypointReached >= enemyController.enemySO.waypointWaitTime)
            {
                index++;
                if (index == enemyController.patrolPath.transform.childCount)
                {
                    index = 0;
                }
                timeSinceWaypointReached = 0;
            }
        }

        // Checks if enemy can chase, if true changes state to Chasing
        if(CanChase()) { enemyController.ChangeState(State.Chase); }
        base.Update();
    }
   
    public override void Exit()
    {
        base.Exit();
        enemyController.agent.speed = enemyController.enemyModel.chaseSpeed; // sets speed of navmesh agent while chasing
    }
}
