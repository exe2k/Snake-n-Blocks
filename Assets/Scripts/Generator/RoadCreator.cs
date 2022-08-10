using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathCreation.Builder {

    [RequireComponent(typeof(PathCreator))]
    public class RoadCreator : MonoBehaviour {

        public bool closedLoop = false; //default false

        public LinkedList<Transform> waypoints = new LinkedList<Transform>();


        public void GeneratePath()
        {
            var wp_array = waypoints.ToArray();
            print(waypoints.Count);

            if (waypoints.Count > 0)
            {
                BezierPath bezierPath = new BezierPath(waypoints.ToArray(), closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
        }
    }
}