using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;


namespace PathCreation.Builder
{
    /// <summary>
    /// Responsible for waypoints generation. 
    /// Should be run first in stack!
    /// </summary>

    public class WaypointsGenerator : WorldGeneratable
    {
        public LinkedList<GameObject> Waypoints = new LinkedList<GameObject>();
        [Min(2)]
        public int WayPointsAmount = GEN_MIN_WAYPOINTS;

        void Start()
        {
            Clear();
            CreatePath(WayPointsAmount);
        }

        public void CreatePath(int NumOfPoints)
        {
            for (int i = 0; i < NumOfPoints; i++)
            {
                Waypoints.AddLast(GenerateNewWaypoint());
            }
        }

        private GameObject GenerateNewWaypoint()
        {
            var wp = GameObject.CreatePrimitive(PrimitiveType.Sphere);


            var lastWP = (Waypoints.Count>0)? Waypoints.Last.Value : null;

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
            return wp;
        }

        private void RandomizeWP(GameObject wp)
        {

            var x = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);
            var y = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);
            var z = Random.Range(GEN_MIN_WP_DISTANCE, GEN_MAX_WP_DISTANCE);

            var xRot = Random.Range(-GEN_WP_ANGLE_RND, GEN_WP_ANGLE_RND);
            var yRot = Random.Range(-GEN_WP_ANGLE_RND, GEN_WP_ANGLE_RND);
            var zRot = Random.Range(-GEN_WP_ANGLE_RND, GEN_WP_ANGLE_RND);

            var newPos = new Vector3(x, y, z);
            var newRot = new Vector3(xRot, yRot, zRot); 

            //TODO: Add rotation chance!

            wp.transform.position = newPos;
            wp.transform.Rotate(newRot);
        }



    }
}