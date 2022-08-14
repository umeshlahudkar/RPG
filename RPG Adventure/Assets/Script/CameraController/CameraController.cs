using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void LateUpdate()
    {
        if(PlayerService.Instance.activePlayer != null)
             gameObject.transform.position = PlayerService.Instance.activePlayer.transform.position;
    }
}
