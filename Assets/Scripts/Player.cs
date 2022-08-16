using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Builder;

/// <summary>Player Main Class</summary>

[RequireComponent(typeof(PathFollower))]

public class Player : MonoBehaviour
{
    public static Player instance;
    PathFollower pathFollower;

    public bool isControlsOn = true;
    private int points = 1;
    LinkedList<Transform> links = new LinkedList<Transform>();

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
        LinksMove();
    }

    private void Controls()
    {
        var mouse = Input.mousePosition.normalized.x * 2;
        head.transform.localPosition = new Vector3(0, 0, mouse);
    }

    private void LinksMove()
    {
        foreach (var link in links)
        {
            link.transform.position += new Vector3(0, 11, 0);
         }
    }

    public void AddPoints(int val)
    {
        points += val;
        print(points);
        AddLink();
    }

    public void TakeDamage(int val)
    {
        points -= val;
        print(points);
        if (points < 1)
            Die();
    }

    private void AddLink()
    {
        var link = new GameObject().transform;
        link.name = "Link" + links.Count;
        links.AddLast(link);
       
        var linkPF = link.gameObject.AddComponent<PathFollower>();
        linkPF.pathCreator = pathFollower.pathCreator;
        linkPF.speed = speed;
        linkPF.distanceTravelled = pathFollower.distanceTravelled-(1.2f*links.Count);

        var linkInnerMesh = Instantiate(linksMesh,link);
        linkInnerMesh.localPosition = container.localPosition;
    }

    private void Die()
    {
        print("died");
        isControlsOn = false;
    }
}
