using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    public ArrowController arrowController { get; private set; }
    float timeSinceEnable = 0;
    void Update()
    {
        arrowController.Move();

        timeSinceEnable += Time.deltaTime;
        if (timeSinceEnable >= arrowController.arrowModel.timeToDisable)
        {
            timeSinceEnable = 0;
            arrowController.Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(arrowController.arrowModel.weaponDamage, arrowController.characterType);
        }

        arrowController.Disable();
    }

    public void SetArrowController(ArrowController arrowController)
    {
        this.arrowController = arrowController;
    }
}
