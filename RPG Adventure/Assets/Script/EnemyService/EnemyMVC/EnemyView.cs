using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour,IDamageable
{
    public EnemyController enemyController { get; private set; }

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject bow;
    [SerializeField] Transform weaponPosition;
    [SerializeField] GameObject healthBar;
    [SerializeField] Slider healthBarSlider;

    Coroutine coroutine;

    private void Start()
    {
        healthBarSlider.maxValue = enemyController.enemyModel.health;
        enemyController.SetReferences(agent, animator, sword, bow, weaponPosition, healthBarSlider);
        enemyController.UpdateHealthBar();
        enemyController.SetEnemyAnimator();
        enemyController.ChangeState(State.Idle);
    }

    private void Update()
    {
        enemyController.LookTowardCamera(healthBar);
        enemyController.ProcessState();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(enemyController.enemyModel.damagePoint, enemyController.characterType);
        }
    }

    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    void Hit() { }

    void Shoot() { }

    public void TakeDamage(int damage, CharacterType characterType)
    {
        enemyController.TakeDamage(damage, characterType);
        DisplayHealthBar();
    }

    public void DisplayHealthBar()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ActivateHealthBar());
    }

    public IEnumerator ActivateHealthBar()
    {
        healthBar.SetActive(true);
        yield return new WaitForSeconds(3);
        healthBar.SetActive(false);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
