using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMover : MonoBehaviour
{
    const float offset= 2f; 
    int direction = 1;
    float curXpos; 
    float speed = 1.4f;
    float speedSalt;


    private void Start()
    {
        curXpos = transform.localPosition.x;
        speedSalt = Random.Range(-0.5f, 0.5f);
    }

    private void Update()
    {
        curXpos += (speed+speedSalt)* Time.deltaTime * direction; 
        
        if (curXpos >= offset)
        {
            direction *= -1;
            curXpos = offset;
        }
        else if (curXpos <= -offset)
        {
            direction *= -1;
            curXpos = -offset;
        }

        transform.localPosition = new Vector3(curXpos, 0);
    }
}
