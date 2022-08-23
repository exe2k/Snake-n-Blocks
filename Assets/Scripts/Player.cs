using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Builder;
using PathCreation;

/// <summary>Player Main Class</summary>

[RequireComponent(typeof(PathFollower))]

public class Player : MonoBehaviour
{
    public static Player instance;
    PathFollower pathFollower;

    private bool isControlsOn = true;
    public bool isAlive = true;
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
        pathFollower.pathCreator = FindObjectOfType<PathCreator>();

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
        if (!isControlsOn)
        {
            speed *= 0;
            return;
        }
        else
        {
            speed *= 1;
            var mouse = Input.mousePosition.normalized.x * 2;
            head.transform.localPosition = new Vector3(0, mouse);
        }
    }


    private void LinksMove()
    {
        for (int i = 1; i < links.Count; i++)
        {
            var curLink = links[i];
            var prevLink = links[i - 1];

            float dis = Vector3.Distance(curLink.position, prevLink.position);
            Vector3 newPos = prevLink.position;

            float t = Time.deltaTime * dis / CONST.P_LINKS_OFFSET * speed ;
            if (t > CONST.P_LINKS_OFFSET) t = CONST.P_LINKS_OFFSET;

            var innerPart = curLink.GetChild(0);
            var innerPartPrev = (i == 1) ? head : prevLink.GetChild(0);

            if (innerPart != null)
                innerPart.localPosition = Vector3.Slerp(innerPart.localPosition, innerPartPrev.localPosition, t);

            //to be ON the road, not stucked in the mid
            innerPart.localPosition = new Vector3(-.5f, innerPart.localPosition.y, innerPart.localPosition.z);
            
            /* 
              curLink.position = Vector3.Slerp(curLink.position, newPos, t);
              curLink.rotation = Quaternion.Slerp(curLink.rotation, prevLink.rotation, t);
            */
        }

    }

    public void AddPoints(int val)
    {
        points += val;
        for (int i = 0; i < val; i++)
        {
            AddLink();
        }
        
    }

    public void TakeDamage()
    {
        points--;

        if (points < 1)
        {
            Die();
            return;
        }

        RemoveLink();
    }

    //move back on damage
    public void HitBack()
    {
        foreach (var l in links)
        {
            l.GetComponent<PathFollower>().distanceTravelled -= CONST.P_LINKS_OFFSET * 3.3f;
        }
    }

    public void Finish()
    {
        print("Done");
        isControlsOn = false;
    }

    private void RemoveLink()
    {
        if (links.Count < 2) return;

        Transform removed = links[links.Count - 1];
        links.RemoveAt(links.Count - 1);
        Destroy(removed.gameObject);
    }

    private void AddLink()
    {
        var link = new GameObject().transform;
        link.name = "Link" + links.Count;
        link.localRotation = container.localRotation;
        links.Add(link);
        var linkInnerMesh = Instantiate(linksMesh, link);
        linkInnerMesh.localPosition = container.localPosition;

        //automatic move

        var linkPF = link.gameObject.AddComponent<PathFollower>();
        linkPF.pathCreator = pathFollower.pathCreator;
        linkPF.speed = speed;
        linkPF.distanceTravelled = pathFollower.distanceTravelled - CONST.P_LINKS_OFFSET * links.Count+1;
    }

    private void Die()
    {
        print("died");
        isControlsOn = false;
        isAlive = false;
    }
}
