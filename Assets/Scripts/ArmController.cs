using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArmController : MonoBehaviour
{
    [SerializeField] Sprite armSprite, parentSprite;
    float lifetime;
    [SerializeField] float initialLifeTime, targetscale = 2.5f;
    [SerializeField] new BoxCollider2D collider;
    float scaleTimer, difference;
    void Start()
    {
        lifetime = initialLifeTime / 2;
        difference = 2 / initialLifeTime * targetscale;
        scaleTimer = initialLifeTime * difference - targetscale;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        scaleTimer -= Time.deltaTime * difference;
        if (lifetime <= initialLifeTime / 2 - initialLifeTime)
        {
            Destroy(gameObject);
        }

        // Places the arm at the horizontal edge of the player sprite

        transform.localScale = new(targetscale - Mathf.Abs(scaleTimer), transform.localScale.y);
        float armOffset = (parentSprite.bounds.size.x / 2) + armSprite.bounds.size.x / 2 * transform.localScale.x;
        transform.localPosition = new(armOffset, parentSprite.bounds.size.y * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        collider.enabled = false;
        scaleTimer *= -1;
        lifetime *= -1;
    }
}
