using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ArmController : MonoBehaviour
{
    [SerializeField]
    Sprite armSprite;

    [SerializeField]
    Sprite parentSprite;

    [SerializeField]
    float lifetime = 0.3f;

    [SerializeField]
    new BoxCollider2D collider;

    [SerializeField]
    float targetscale = 2.5f;
    float scaleTimer;
    float difference;
    bool hasPerformedCollisionCheck = false;
    void Start()
    {
        difference = 2 / lifetime * targetscale;
        scaleTimer = lifetime * difference - targetscale;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        scaleTimer -= Time.deltaTime * difference;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        if (scaleTimer <= 0 && !hasPerformedCollisionCheck)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }

        // Places the arm at the horizontal edge of the player sprite

        gameObject.transform.localScale = new(targetscale - Mathf.Abs(scaleTimer), gameObject.transform.localScale.y);
        float armOffset = (parentSprite.bounds.size.x / 2) + armSprite.bounds.size.x / 2 * gameObject.transform.localScale.x;
        gameObject.transform.localPosition = new(armOffset, parentSprite.bounds.size.y * 0.1f);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        {
            if(scaleTimer <= 0)
            {
                hasPerformedCollisionCheck = true;
            }
        }
    }
}
