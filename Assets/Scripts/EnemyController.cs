using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LargeEnemyController : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    GameObject player;

    [SerializeField]
    int hitpoints = 2;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        HealthBarDisplayController.RenderHealthBar(transform.position, hitpoints);
        HandleWalking();
    }

    private void HandleWalking()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.Translate(new(walkSpeed * Time.deltaTime, 0));
        }
        else 
        {
            transform.Translate(new(-walkSpeed * Time.deltaTime, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
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
