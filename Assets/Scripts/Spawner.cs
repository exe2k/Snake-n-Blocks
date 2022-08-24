using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

[RequireComponent(typeof(Optimizer))]
public class Spawner : MonoBehaviour
{
    public Obstacle[] obstacles;
    public Food[] food;
    public float distance = 0;
    [SerializeField] Transform contrainer;
    int spawnedObjectsCounter = 0;
    GameObject currentObject = null;

    float[] posOffset = { -1.5f, 0, 1.5f };

    bool singleSpawn = false;
    bool fullBad = false;

    private void Start()
    {
        if (contrainer == null) transform.GetChild(0);
        Spawn();

        GetComponent<Optimizer>().distance = distance;
    }


    private void Spawn()
    {
        fullBad = SPAWNER_BAD_ONLY_CHANCE > Random.Range(0, 100);
        PopulateContainer();
    }

    private void PopulateContainer()
    {
        Collectable prefab = null;

        if (distance < SPAWNER_OBSTACLES_START_DISTANCE)
            prefab = GetRandomPrefab(food);
        else if (fullBad == true)
            prefab = GetRandomPrefab(obstacles);
        else
            prefab = GetRandomPrefab(food, obstacles);



        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(1, 3) > 1 && !fullBad) continue;
            var p = Instantiate(prefab, contrainer);
            p.transform.localPosition = new Vector3(posOffset[i], 0, 0);
            spawnedObjectsCounter++;
            currentObject = p.gameObject;
        }


        if (spawnedObjectsCounter == 1 && SPAWNER_MOVING_CHANCE > Random.Range(1, 100))
        {
            try { currentObject.GetComponent<Collectable>().isMoving = true; }
            catch { Debug.LogWarning(" Can't set 'isMoving' to not Collactable object!"); }
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
        return (rndList == 1) ? list[Random.Range(0, list.Length)] : list2[Random.Range(0, list.Length)];
    }
}
