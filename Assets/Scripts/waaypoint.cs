using UnityEngine;

public class waaypoint : MonoBehaviour
{
    [SerializeReference]  float radius;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
