using UnityEngine;
using PathCreation.Builder;

/// <summary>Player Main Class</summary>

[RequireComponent(typeof(PathFollower))]

public class Player : MonoBehaviour
{
    public static Player instance;
    PathFollower pathFollower;
    public bool isControlsOn = true;
    [SerializeField] float speed = CONST.P_BASIC_SPEED;
    [SerializeField] Transform headMesh;
    [SerializeField] Transform linksMesh;

    private void Awake()
    {
        pathFollower = GetComponent<PathFollower>();
    }

    private void Update()
    {
        pathFollower.speed = speed;

    }


    private void Controls()
    {
        if (Input.GetKey(KeyCode.A))
        {

        }
    }
}
