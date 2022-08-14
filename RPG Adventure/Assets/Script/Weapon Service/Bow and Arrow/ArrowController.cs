using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController 
{
    public ArrowView arrowView { get; private set; }
    public ArrowModel arrowModel { get; private set; }
    public  CharacterType characterType;

    public ArrowController(ArrowModel _arrowModel, GameObject arrowPrefab, Transform spwanPosition, CharacterType characterType)
    {
        arrowModel = _arrowModel;
        arrowView = GameObject.Instantiate(arrowPrefab, spwanPosition.position, spwanPosition.rotation).GetComponent<ArrowView>();

        arrowModel.SetArrowController(this);
        arrowView.SetArrowController(this);
        this.characterType = characterType;
    }

    public void Move()
    {
        arrowView.transform.Translate(Vector3.forward * Time.deltaTime * arrowModel.speed);
    }

    public void Enable(Transform position)
    {
        arrowView.gameObject.transform.position = position.position;
        arrowView.gameObject.transform.rotation = position.rotation;
        arrowView.gameObject.SetActive(true);
    }

    public void Disable()
    {
        arrowView.gameObject.SetActive(false);
        ServicePool<ArrowController>.Instance.ReturnToPool(this, characterType);
    }
}
