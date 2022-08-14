using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    // visualize the waypoints and connects the two waypoints via line 
    private void OnDrawGizmos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            int j = i + 1;
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.3f);
            if(j == transform.childCount)
            {
                j = 0;
            }
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(j).position);
        }
    }
}
