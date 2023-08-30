using UnityEngine;

public class HitColliders : MonoBehaviour
{
    void Update()
    {
       {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0f);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("inside");
        }
    } 
    }
}