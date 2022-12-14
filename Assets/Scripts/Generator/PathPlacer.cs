using PathCreation;
using UnityEngine;

namespace PathCreation.Builder
{

    public class PathPlacer : PathSceneTool
    {

        public GameObject prefab;
        public GameObject finish;
        public float spacing = 3;
        [SerializeField]
        [Min(0)]
        const float minSpacing = .1f;

        public void Generate()
        {
            DestroyObjects();
            if (pathCreator != null && prefab != null)
            {

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = 0;

                while (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    Quaternion rot = path.GetRotationAtDistance(dst);
                    dst += spacing;
                    if (path.length - dst < .2f)
                        Instantiate(finish, point, rot, transform);
                    else if (dst > CONST.START_EMPTY_SPACE)
                    {
                        var spawner = Instantiate(prefab, point, rot, transform);
                        spawner.GetComponent<Spawner>().distance = dst;
                    }
                }
            }
            else
            {
                Debug.LogError("No path creator or prefab");
            }
    
        }

        void DestroyObjects()
        {
            int numChildren = transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject, false);
            }
        }

        protected override void PathUpdated()
        {
            // if (pathCreator != null) 
            //  Generate ();
        }

    }
}