using UnityEngine;
using System.Collections;
using System.Linq;
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
    public float distance { get; private set; }
    public float prevDistance { get; private set; }

    List<Transform> links = new List<Transform>();
    Dictionary<float, float> checkpoints = new Dictionary<float, float>();

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
        links.Add(Player.instance.transform);

    }

    private void Update()
    {
        pathFollower.speed = speed;
        distance = pathFollower.distanceTravelled;

        Controls();
        LinksMove();
    }

    private void Controls()
    {
        var mouse = Input.mousePosition.normalized.x * 2;
        head.transform.localPosition = new Vector3(0, mouse);
    }


    private void LinksMove()
    {
        for (int i = 1; i < links.Count; i++)
        {
            var curLink = links[i];
            var prevLink = links[i - 1];

            float dis = Vector3.Distance(curLink.position, prevLink.position);
            Vector3 newPos = prevLink.position;


            float t = Time.deltaTime * dis / CONST.P_LINKS_OFFSET * speed;

            if (t > 2) t = 2;

            var innerPart = curLink.GetChild(0);
            var innerPartPrev = (i == 1) ? head : prevLink.GetChild(0);
            
            if(innerPart!=null)
                innerPart.localPosition = Vector3.Slerp(innerPart.localPosition, innerPartPrev.localPosition, t);

            //to be ON the road, not inside
            innerPart.localPosition = new Vector3(-.5f, innerPart.localPosition.y, innerPart.localPosition.z);

            curLink.position = Vector3.Slerp(curLink.position, newPos, t);
            curLink.rotation = Quaternion.Slerp(curLink.rotation, prevLink.rotation, t);
        }

    }

    public void AddPoints(int val)
    {
        points += val;
        AddLink();
    }

    public void TakeDamage(int val)
    {
        points -= val;
        if (points < 1)
            Die();
    }

    private void AddLink()
    {
        var link = new GameObject().transform;
        link.name = "Link" + links.Count;
        link.localRotation = container.localRotation;
        links.Add(link);
        var linkInnerMesh = Instantiate(linksMesh, link);
        linkInnerMesh.localPosition = container.localPosition;

        /*  //automatic move
          
         var linkPF = link.gameObject.AddComponent<PathFollower>();
         linkPF.pathCreator = pathFollower.pathCreator;
         linkPF.speed = speed;
         linkPF.distanceTravelled = pathFollower.distanceTravelled - CONST.P_LINKS_OFFSET * links.Count;
        */

    }

    private void Die()
    {
        print("died");
        isControlsOn = false;
    }
}
