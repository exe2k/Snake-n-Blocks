using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : Collectable
{
    protected override void Init()
    {
        Validate();
        UpdateVisual();
    }

    protected override void CollectExtraCode()
    {
        print("hit Player");
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.OBSTACLE_MAX_POINTS);
    }

    private void Validate()
    {
        var objs = transform.parent.GetComponentsInChildren<Obstacle>();
        if (objs.Length > 2 && Random.Range(1,10)>5)
        {
           objs[0].points = Mathf.Clamp(points, 1, CONST.OBSTACLE_MAX_POINTS / 5);
        }
    }


    private void UpdateVisual()
    {
        var mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Points", points);
        mat.SetFloat("_MaxPoints", CONST.OBSTACLE_MAX_POINTS);

        UpdateText();
    }
}
