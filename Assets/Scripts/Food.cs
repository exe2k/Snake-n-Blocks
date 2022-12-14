using UnityEngine;

public class Food : Collectable
{
    protected override void Init()
    {
        Rotator rot = GetComponentInChildren<Rotator>();
        var rnd = Random.Range(1f, 3f);
        if (rot != null)
            rot.AnglePerSecond *= rnd;
    }
    protected override void CollectExtraCode()
    {
        Player.instance.AddPoints(points);
        var children = gameObject.GetComponentsInChildren<MeshFilter>();
        foreach (var child in children)
        {
            child.gameObject.SetActive(false);
        }
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.FOOD_MAX_POINTS);
    }


}
