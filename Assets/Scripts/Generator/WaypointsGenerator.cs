using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

/// <summary>
/// Responsible for waypoints generation. 
/// Should be run first in stack!
/// </summary>

public class WaypointsGenerator : WorldGeneratable
{
    public LinkedList<Transform> waypoints = new LinkedList<Transform>();
    [Min(2)] 
    public int wayPointsAmount = 1;

    public void CreateWaypoints()
    {
        wayPointsAmount += GEN_MIN_WAYPOINTS;

        Clear();
        for (int i = 0; i < wayPointsAmount; i++)
        {
            waypoints.AddLast(GenerateNewWaypoint(i.ToString()));
        }

        SetZeroRotation(waypoints.Last.Value);
        SetZeroRotation(waypoints.First.Value);
        SetZeroRotation(waypoints.First.Next.Value);

        WorldHandler.instance.isWaypointsReady = true;

    }

    private Transform GenerateNewWaypoint(string name="")
    {
        var wp = new GameObject("WayPoint_"+name);
        var lastWP = (waypoints.Count > 0) ? waypoints.Last.Value : null;

        if (lastWP != null)
        {
            wp.transform.position = lastWP.transform.position;
            wp.transform.rotation = lastWP.transform.rotation;
            RandomizeWP(wp);
        }
        else
        {
            wp.transform.position = Vector3.zero;
        }

        wp.transform.SetParent(transform);
        return wp.transform;
    }

    private void RandomizeWP(GameObject wp)
    {

        var x = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);
        var y = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);
        var z = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);

        var newPos = new Vector3(x+ wp.transform.position.x , y , z );

        wp.transform.position = newPos;

        if (!Validate(wp.transform))
        {
            RandomizeWP(wp);
        }

   }

    private void SetZeroRotation(Transform wp)
    {
        wp.rotation = Quaternion.Euler(Vector3.zero);
    }

    private bool Validate(Transform wp)
    {
        foreach (var item in waypoints)
        {
            var dist = Vector3.Distance(wp.transform.position, item.position);
            return (dist > GEN_MIN_WP_DISTANCE) ? true : false;
        }
        return true;
    }



}
