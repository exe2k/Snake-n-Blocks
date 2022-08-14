using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

public class Spawner : MonoBehaviour
{
    public Obstacle[] obstacles;
    public Food[] food;
    public float distance = 0;
    [SerializeField] Transform contrainer;

    float[] posOffset = { -1.5f, 0, 1.5f };

    private void Start()
    {
        if (contrainer == null) transform.GetChild(0);
        Spawn();
    }


    private void Spawn()
    {
        bool singleSpawn = SPAWNER_SINGLE_OBJ_CHANCE < Random.Range(0, 100);

        if (distance < SPAWNER_OBSTACLES_START_DISTANCE)
        {
            for (int i = 0; i < Random.Range(1,4); i++)
            {
                var p = Instantiate(GetRandomPrefab(food), contrainer);
                p.transform.localPosition = new Vector3(posOffset[i], 0, 0);
            }
           
        }
        else
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                var p = Instantiate(GetRandomPrefab(food,obstacles), contrainer);
                p.transform.localPosition = new Vector3(posOffset[i], 0, 0);
            }

        }
    }

    private Collectable GetRandomPrefab(Collectable[] list)
    {
        var rnd_index = Random.Range(0, list.Length);
            return list[rnd_index];
    }

    private Collectable GetRandomPrefab(Collectable[] list, Collectable[] list2)
    {
        var rndList = Random.Range(0, 2);
        return (rndList==1)? list[Random.Range(0, list.Length)]: list2[Random.Range(0, list.Length)];
    }
}
