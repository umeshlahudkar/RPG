using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController 
{
    public EnemyModel enemyModel { get; private set; }
    public EnemyView enemyView { get; private set; }
    public EnemySO enemySO { get; private set; }
    public EnemyState currentState { get; private set; }
    public GameObject patrolPath { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }
    public GameObject sword { get; private set; }
    public GameObject bow { get; private set; }
    public Transform weaponPosition { get; private set; }
    private Slider healthBarSlider;
    public CharacterType characterType = CharacterType.Enemy;

    public EnemyController(EnemyModel _enemyModel,EnemySO _enemySO, GameObject _patrolPath, CharacterType characterType)
    {
        enemyModel = _enemyModel;
        enemyView = GameObject.Instantiate<EnemyView>(_enemySO.enemyView);
        enemyView.transform.position = _patrolPath.transform.GetChild(0).position;
        enemyView.SetEnemyController(this);
        patrolPath = _patrolPath;
        enemySO = _enemySO;
        SetMaterialColour();
        this.characterType = characterType;

        EventService.Instance.OnPlayerDied += OnGameOver;
    }

    public void OnGameOver()
    {
        enemyView.enabled = false;
    }

    public async void OnDie()
    {
        animator.SetTrigger("die");
        enemyView.gameObject.GetComponent<Collider>().enabled = false;
        currentState = null;
        EventService.Instance.InvokeOnEnemyDeadEvent();
        SoundManager.Instance.PlaySFXSound(SoundType.Dead);

        await Task.Delay(System.TimeSpan.FromSeconds(2));

        enemyView.gameObject.SetActive(false);
        ServicePool<EnemyController>.Instance.ReturnToPool(this, characterType);
    }


    // Override animator based on weapon equipped
    public void SetEnemyAnimator()
    {
        switch(enemySO.weaponType)
        {
            case WeaponType.Sword:
                sword.SetActive(true);
                animator.runtimeAnimatorController = enemySO.animatorOverrideController;
                break;

            case WeaponType.Bow:
                bow.SetActive(true);
                animator.runtimeAnimatorController = enemySO.animatorOverrideController;
                break;

            case WeaponType.FireBall:
                animator.runtimeAnimatorController = enemySO.animatorOverrideController;
                break;
        }
    }

    // Update the health After taking Damage
    public void TakeDamage(int damage, CharacterType characterType)
    {
        if (this.characterType == characterType) return;

        int health = enemyModel.health - damage;
        enemyModel.SetHealth(health);
        UpdateHealthBar();
        if (enemyModel.health <= 2)
        {
            OnDie();
        }
    }

    // Upate the HealthBar
    public void UpdateHealthBar()
    {
        healthBarSlider.value = enemyModel.health;
    }

    // Healthbar world space Canvas looking toward the camera 
    public void LookTowardCamera(GameObject healthBarCanvas)
    {
        healthBarCanvas.transform.LookAt(Camera.main.transform);
    }

    // Runs the Update of the states
    public void ProcessState()
    {
        if(currentState != null)
            currentState.Update();
    }

    // Change states
    public void ChangeState(State enemyState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = GetNewState(enemyState);
        currentState.Enter();
    }

    // based on states(enum) returning the new state
    private EnemyState GetNewState(State enemyState)
    {
        EnemyState newState;
        switch (enemyState)
        {
            case State.Idle:
                newState = new EnemyIdleState(this);
                return newState;

            case State.Patrol:
                newState = new EnemyPatrolState(this);
                return newState;

            case State.Chase:
                newState = new EnemyChaseState(this);
                return newState;

            case State.Attack:
                newState = new EnemyAttackState(this);
                return newState;
        }

        return null;
    }

    // set the references
    public void SetReferences(NavMeshAgent agent, Animator animator, GameObject sword, GameObject bow, Transform weaponPosition, Slider healthbarSlider )
    {
        this.agent = agent;
        this.animator = animator;
        this.sword = sword;
        this.bow = bow;
        this.weaponPosition = weaponPosition;
        this.healthBarSlider = healthbarSlider;
    }

    // sets the material of the player for different player
    private void SetMaterialColour()
    {
        Renderer[] renderers = enemyView.gameObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.material = enemySO.material;
        }
    }

    ~EnemyController()
    {
        EventService.Instance.OnPlayerDied -= OnGameOver;
    }
}
