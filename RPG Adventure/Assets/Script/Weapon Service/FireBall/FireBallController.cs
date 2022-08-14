using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController 
{
    public FireBallView fireBallView { get; private set; }
    public FireBallModel fireBallModel { get; private set; }

    public CharacterType characterType;

    public FireBallController(FireBallModel _fireBallModel, GameObject fireBall, Transform spwnPosition, CharacterType characterType)
    {
        fireBallModel = _fireBallModel;
        fireBallView = GameObject.Instantiate(fireBall, spwnPosition.position, spwnPosition.rotation).GetComponent<FireBallView>();

        fireBallModel.SetFireController(this);
        fireBallView.SetFireController(this);
        this.characterType = characterType;
    }

    public void Disable()
    {
        fireBallView.gameObject.SetActive(false);
        ServicePool<FireBallController>.Instance.ReturnToPool(this, characterType);
    }

    public void Enable(Transform position)
    {
        fireBallView.gameObject.SetActive(true);
        fireBallView.gameObject.transform.position = position.position;
        fireBallView.gameObject.transform.rotation = position.rotation;
    }
    public void Move()
    {
        fireBallView.transform.Translate(Vector3.forward * Time.deltaTime * fireBallModel.speed);
    }
}
