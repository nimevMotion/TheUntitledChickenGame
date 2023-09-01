using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(FovRoutine());

    }

    IEnumerator FovRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true)
        {
            yield return wait;
            FielOfViewCheck();
        }
    }

    private void FielOfViewCheck()
    {
        Collider[] rangeObjs = Physics.OverlapSphere(transform.position, radius, targetMask);
        
        if(rangeObjs.Length != 0)
        {
            Transform target = rangeObjs[0].transform;
            Vector3 directionTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTarget) < angle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionTarget, distance, obstacleMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else 
                canSeePlayer = false;
        }else if (canSeePlayer)
            canSeePlayer = false;
    }
}
