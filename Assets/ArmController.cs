using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    float armDuration = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        armDuration -= Time.deltaTime;
        if(armDuration <= 0)
        {
            Destroy(this);
        }
    }
}
