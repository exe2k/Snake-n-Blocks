using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CONST;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 1;
    
    void Start()
    {
        if (instance != null) instance = this;
        else Destroy(gameObject);

        Init();
    }

    private void Init()
    {
       if(RND_SEED>0) Random.InitState(RND_SEED);
    }

    void Update()
    {
        
    }
}
