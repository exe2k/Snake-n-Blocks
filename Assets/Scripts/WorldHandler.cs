using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Builder;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler instance;

    RoadCreator roadCreator;
    WaypointsGenerator waypointsGenerator;
    public bool isWaypointsReady = false;
    public bool isPathReady = false;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        roadCreator = FindObjectOfType<RoadCreator>();
        waypointsGenerator = FindObjectOfType<WaypointsGenerator>();

        StartCoroutine(Init());
    }


    IEnumerator Init()
    {
        waypointsGenerator.CreateWaypoints();

        if(!isWaypointsReady)
        yield return new WaitUntil(() => isWaypointsReady);
        
        roadCreator.waypoints = waypointsGenerator.waypoints;
        roadCreator.GeneratePath();
        isPathReady = true;
    }
}

