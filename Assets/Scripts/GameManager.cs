using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 1;

    [SerializeField] WorldHandler world;
    [SerializeField] Player player;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Init();
    }

    private void Init()
    {
        if (RND_SEED > 0) Random.InitState(RND_SEED);

        world.GM = this;
        var w = Instantiate(world);
        StartCoroutine(WaitForWorld());
    }

    IEnumerator WaitForWorld()
    {
        if (WorldHandler.instance==null || WorldHandler.instance.isPathReady==false )
            yield return 0;

        Instantiate(player);
    }
}
