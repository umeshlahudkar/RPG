using System;
using UnityEngine;

public class FireBallView : MonoBehaviour
{
    public FireBallController fireBallController { get; private set; }
    float timeSinceEnable = 0;
    private void Update()
    {
        fireBallController.Move();

        timeSinceEnable += Time.deltaTime;
        if (timeSinceEnable >= fireBallController.fireBallModel.timeToDisable)
        {
            timeSinceEnable = 0;
            fireBallController.Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(fireBallController.fireBallModel.weaponDamage, fireBallController.characterType);
            fireBallController.Disable();
        }

    }

    public void SetFireController(FireBallController fireBallController)
    {
        this.fireBallController = fireBallController;
    }
}
