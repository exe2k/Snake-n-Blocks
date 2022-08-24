using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Builder;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler instance;

    RoadCreator roadCreator;
    WaypointsGenerator waypointsGenerator;
    PathPlacer pathPlacer;
    public GameManager GM;
    public bool isWaypointsReady = false;
    public bool isPathReady = false;

    
    void Start()
    {
        if (instance != null) Destroy(instance.gameObject); 
        instance = this;

        if(GM==null) GM = FindObjectOfType<GameManager>();

        roadCreator = FindObjectOfType<RoadCreator>();
        pathPlacer = FindObjectOfType<PathPlacer>();
        waypointsGenerator = FindObjectOfType<WaypointsGenerator>();
        waypointsGenerator.wayPointsAmount = Mathf.Clamp(GM.level,CONST.GEN_MIN_WAYPOINTS,CONST.GEN_MAX_WAYPOINTS);
        StartCoroutine(Init());
    }


    IEnumerator Init()
    {
        waypointsGenerator.wayPointsAmount = GM.level;
        waypointsGenerator.CreateWaypoints();

        if(!isWaypointsReady && GameManager.instance!=null)
        yield return new WaitUntil(() => isWaypointsReady);
        
        roadCreator.waypoints = waypointsGenerator.waypoints;
        roadCreator.GeneratePath();
        pathPlacer.Generate();

        isPathReady = true;
    }
}

