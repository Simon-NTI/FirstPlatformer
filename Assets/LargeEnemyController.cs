using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyController : MonoBehaviour
{
    float turnAroundTimer = 0;
    [SerializeField]
    float turnAroundTreshhold = 3;

    [SerializeField]
    float walkSpeed = 5;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleWalking();
    }

    private void HandleWalking()
    {
        turnAroundTimer += Time.deltaTime;
        if(turnAroundTimer >= turnAroundTreshhold)
        {
            walkSpeed *= -1;
            turnAroundTimer = 0;
        }
        transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new(transform.position.x + walkSpeed * turnAroundTreshhold / 2, transform.position.y), new(walkSpeed * turnAroundTreshhold, 0.3f));
    }
}
