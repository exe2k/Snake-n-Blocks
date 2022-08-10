using UnityEngine;

namespace PathCreation.Builder {

    [RequireComponent(typeof(PathCreator))]
    public class Builder_road : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;

        void Start ()
        {
            GeneratePath();
        }

        public void GeneratePath()
        {
            if (waypoints.Length > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
        }
    }
}