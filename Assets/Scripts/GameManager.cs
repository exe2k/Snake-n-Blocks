using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CONST;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 1;

    [SerializeField] WorldHandler world;
    [SerializeField] Player player;
    Player currentPlayer;

    public UnityEvent<GameStates> OnStateChanged = new UnityEvent<GameStates>();

    public enum GameStates { start=-1, game=0, win=1, lose=2 };
    private GameStates _state;
    public GameStates state
    {
        get => _state;
         
        set
        {
          //if (value == _state) return;
          _state = value;
          OnStateChanged?.Invoke(state);
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        OnStateChanged.AddListener(SwitchState);
        int loadlevel = PlayerPrefs.GetInt("Level");
        level = (loadlevel>0)? loadlevel: 1;
    }

    private void SwitchState(GameStates st)
    {
       //just in case;
    }

    public void SetState(int val)
    {
        state = (GameStates)val;
    }

    public void SetState(GameStates val)
    {
        state = val;
    }

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Init();
    }
    
    public void Init()
    {
       Random.InitState(RND_SEED+level);

        world.GM = this;

        ClearWorld();
        var w = Instantiate(world);
        StartCoroutine(WaitForWorld());
        state = GameStates.start;

        PlayerPrefs.SetInt("Level", level);
    }

    private void ClearWorld()
    {
        var worlds = FindObjectsOfType<WorldHandler>();
        var players = FindObjectsOfType<Player>();

        foreach (var item in worlds)
        {
            Destroy(item);
        }

        foreach (var item in players)
        {
            Destroy(item);
        }
    }

    IEnumerator WaitForWorld()
    {
        if (WorldHandler.instance == null || WorldHandler.instance.isPathReady == false)
            yield return 5;

        currentPlayer = Instantiate(player);
    }
}
