using UnityEngine;

public class EnemyState 
{
    protected EnemyController enemyController;
    public EnemyState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
  
    public virtual void Enter() { }
    public virtual void Update() { UpdateAnimatorSpeed(); }
    public virtual void Exit() { }

    // Moves the enemy to the destination
    public void MoveTowards(Vector3 destination)
    {
        enemyController.agent.SetDestination(destination);
    }

    // Checks the enemy can chase based on Chase distance
    public bool CanChase()
    {
        float distance = Vector3.Distance(enemyController.enemyView.transform.position, PlayerService.Instance.activePlayer.transform.position);
        return (distance <= enemyController.enemyModel.chaseDistance);
    }

    // Checks the enemy can Attack based on attack distance

    public bool CanAttack()
    {
        float distance = Vector3.Distance(enemyController.enemyView.transform.position, PlayerService.Instance.activePlayer.transform.position);
        return (distance <= enemyController.enemyModel.attackDistance);
    }

    // Plays the idle, walk and run animation based on speed
    public void UpdateAnimatorSpeed()
    {
        Vector3 velocity = enemyController.agent.velocity;
        Vector3 localVelocity = enemyController.agent.transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        enemyController.animator.SetFloat("ForwardSpeed", speed);
    }

    // Plays the Attack animation
    public void TriggerFightAnimation()
    {
        enemyController.animator.SetTrigger("attack");
    }

    public void ResetTriggerFightAnimation()
    {
        enemyController.animator.SetTrigger("stopAttack");
    }
   
}

public enum State
{
    None,
    Idle,
    Chase,
    Patrol,
    Attack
}
