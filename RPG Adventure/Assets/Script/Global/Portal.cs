using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int sceneIndex;
    [SerializeField] GameObject spwanPoint;
    [SerializeField] PortalIdentifier portalIdentifier; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerView>())
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneIndex);

        yield return new WaitForSeconds(2);

        Portal portal = GetOtherPortal();
        SetPlayerPosition(portal);
        Destroy(gameObject);
    }

    private void SetPlayerPosition(Portal portal)
    {
        PlayerView player = GameObject.FindObjectOfType<PlayerView>();
        if (player == null) return;
        player.gameObject.SetActive(false);
        player.gameObject.GetComponent<NavMeshAgent>().Warp(portal.spwanPoint.transform.position);
        player.gameObject.transform.rotation = portal.spwanPoint.transform.rotation;
        player.gameObject.SetActive(true);
    }

    private Portal GetOtherPortal()
    {
        foreach(Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this || portal.portalIdentifier != this.portalIdentifier) continue;

            return portal;
        }
        return null;
    }

    public enum PortalIdentifier
    {
        A,B,C,D
    }
}
