using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : ServicePool<FireBallController>
{
    FireBallModel fireBallModel;
    GameObject fireBallPrefab;
    Transform spwanPosition;
    CharacterType characterType;
    public FireBallController GetItem(FireBallModel _fireBallModel, GameObject fireBall, Transform spwnPosition, CharacterType characterType)
    {
        this.fireBallModel = _fireBallModel;
        this.fireBallPrefab = fireBall;
        this.spwanPosition = spwnPosition;
        this.characterType = characterType;

        return GetItem(characterType);
    }
    protected override FireBallController CreateItem()
    {
        FireBallController fireBallController = new FireBallController(fireBallModel, fireBallPrefab, spwanPosition, characterType);
        return fireBallController;
    }
}
