using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LargeEnemyController : MonoBehaviour
{
    float turnAroundTimer = 0;
    float turnAroundTreshhold = 3;
    float walkSpeed;

    [SerializeField]
    int hitpoints = 2;


    void Start()
    {
        walkSpeed = Random.Range(1f, 4f);
        turnAroundTreshhold = Random.Range(1f, 3f);
    }

    void Update()
    {
        HandleWalking();
    }

    private void HandleWalking()
    {
        // The enemy will walk in one direction for a random amount of time and then turn around
        turnAroundTimer += Time.deltaTime;
        if(turnAroundTimer >= turnAroundTreshhold)
        {
            walkSpeed *= -1;
            turnAroundTimer = 0;
            turnAroundTreshhold = Random.Range(1, 2);
        }
        transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("arm"))
        {
            hitpoints--;
            if(hitpoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
