using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : ServicePool<ArrowController>
{
    ArrowModel arrowModel;
    GameObject arrowPrefab;
    Transform spwanPosition;
    CharacterType characterType;
    public ArrowController GetItem(ArrowModel _arrowModel, GameObject arrowPrefab, Transform spwanPosition, CharacterType characterType)
    {
        this.arrowModel = _arrowModel;
        this.arrowPrefab = arrowPrefab;
        this.spwanPosition = spwanPosition;
        this.characterType = characterType;

        return GetItem(characterType);
    }
    protected override ArrowController CreateItem()
    {
        ArrowController arrowController = new ArrowController(arrowModel, arrowPrefab, spwanPosition, characterType);
        return arrowController;
    }
}
