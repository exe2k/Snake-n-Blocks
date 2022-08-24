using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathCreation.Builder {

    [RequireComponent(typeof(PathCreator))]
     public class RoadCreator : MonoBehaviour {

        public bool closedLoop = false; //default false
        public LinkedList<Transform> waypoints = new LinkedList<Transform>();

        public bool GeneratePath()
        {
            var wp_array = waypoints.ToArray();
            if (waypoints.Count > 0)
            {
                BezierPath bezierPath = new BezierPath(waypoints.ToArray(), closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
                bezierPath.GlobalNormalsAngle = CONST.GLOBAL_NORMAL_ANGLE;
            }

            var roadMesh = GetComponent<RoadMeshCreator>();
            roadMesh.textureTiling = waypoints.Count * CONST.TEXTURE_TILE_FACTOR; 
            roadMesh.CreateRoad();

            return true;
        }
    }
}