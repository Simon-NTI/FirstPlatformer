using System.Collections;
using System.Collections.Generic;
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
    float targetscale = 2.5f;
    float scaleTimer;
    float difference;
    void Start()
    {
        difference = 2 / lifetime * targetscale;
        scaleTimer = lifetime * difference - targetscale;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        scaleTimer -= Time.deltaTime * difference;
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }

        gameObject.transform.localScale = new(targetscale - Mathf.Abs(scaleTimer), gameObject.transform.localScale.y);

        float armOffset = (parentSprite.bounds.size.x / 2) + armSprite.bounds.size.x / 2 * gameObject.transform.localScale.x;
        gameObject.transform.localPosition = new(armOffset, parentSprite.bounds.size.y * 0.1f);
    }
}
