using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

public class Camera : MonoBehaviour
{

    public GameObject player;
    private Vector3 prevPos;
    public bool canFollow = true;
    [HideInInspector] public bool canLookAt = false;


    void Update()
    {
        if (canFollow) Follow();
       // transform.LookAt(player.transform);
    }

    void Follow()
    {
        var newPosition = new Vector3(0, CAM_OFFSET_Y, CAM_OFFSET_Z) + player.transform.position ;
        transform.localPosition = Vector3.Lerp(transform.position, newPosition, 5 * Time.deltaTime);
        if (prevPos != newPosition) prevPos = newPosition;
    }


}
