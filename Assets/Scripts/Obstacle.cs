using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : Collectable
{
    protected override void Init()
    {
        var mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Points", points);
        mat.SetFloat("_MaxPoints", CONST.OBSTACLE_MAX_POINTS);
    }

    protected override void CollectExtraCode()
    {
        print("hit Player");
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.OBSTACLE_MAX_POINTS);
    }
}
