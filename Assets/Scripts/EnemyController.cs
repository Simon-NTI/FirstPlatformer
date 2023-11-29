using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LargeEnemyController : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    GameObject player;

    [SerializeField] Slider defaultHealthBar;
    Slider healthBar;
    [SerializeField] GameObject playerUI;

    [SerializeField] int hitpoints = 2, initialHitpoints = 2;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            healthBar = Instantiate(defaultHealthBar, playerUI.transform);
        }
        catch
        {

        }
    }

    void Update()
    {
        //HealthBarDisplayController.RenderHealthBar(transform.position, hitpoints);
        UpdateHealthBar();
        HandleWalking();
    }

    private void HandleWalking()
    {
        // Walk towards the player
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
                Destroy(healthBar);
                Destroy(gameObject);
            }
        }
    }

    private void UpdateHealthBar()
    {
        if(healthBar)
        {
            healthBar.transform.position = new(transform.position.x, transform.position.y + 2f);
            healthBar.value = hitpoints / initialHitpoints;

            print(hitpoints / initialHitpoints);
        }
    }
}
