using UnityEngine;

public class Food : Collectable
{
    protected override void CollectExtraCode()
    {
        print("Collected! MSG from Extra code section");
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.FOOD_MAX_POINTS);
    }


}
