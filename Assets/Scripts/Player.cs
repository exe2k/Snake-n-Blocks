using UnityEngine;
using PathCreation.Builder;

/// <summary>Player Main Class</summary>

[RequireComponent(typeof(PathFollower))]

public class Player : MonoBehaviour
{
    public static Player instance;
    PathFollower pathFollower;

    public bool isControlsOn = true;
    private int points = 1;

    [SerializeField] float speed = CONST.P_BASIC_SPEED;
    [SerializeField] Transform headMesh;
    [SerializeField] Transform linksMesh;
    [SerializeField] Transform container;

    Transform head;

    private void Start()
    {
        if (Player.instance == null) instance = this;

        pathFollower = GetComponent<PathFollower>();
        if (linksMesh == null && headMesh != null)
            linksMesh = headMesh;

        if (container == null)
            container = transform.Find("Container");

        head = Instantiate(headMesh, container);
        head.gameObject.AddComponent<Rigidbody>().isKinematic = true;

    }

    private void Update()
    {
        pathFollower.speed = speed;
        Controls();
    }


    private void Controls()
    {
        var mouse = Input.mousePosition.normalized.x*2;
        head.transform.localPosition = new Vector3(0, 0, mouse);
    }


    public void AddPoints(int val)
    {
        points += val;
        print(points);
    }

    public void TakeDamage(int val)
    {
        points -= val;
        print(points);
        if (points < 1) 
            Die();

    }

    private void Die()
    {
        print("died");
        isControlsOn = false;
    }
}
