using System.Linq;
using UnityEngine;


public abstract class WorldGeneratable: MonoBehaviour
{
    protected void Clear(string[] ExcludeNames)
    {
        var children = transform.GetComponentsInChildren<Transform>();


        foreach (var obj in children)
        {
            if (obj.parent != transform || obj.gameObject == this.gameObject || ExcludeNames.Contains(obj.name))
                continue;
            Destroy(obj.gameObject);
        }
    }


    protected void Clear(string ExcludeName="")
    {
        var children = transform.GetComponentsInChildren<Transform>();

        foreach (var obj in children)
        {

            if (obj.parent != transform || obj.gameObject == this.gameObject || ExcludeName == obj.name)
                continue;
            Destroy(obj.gameObject);
        }
    }
}
