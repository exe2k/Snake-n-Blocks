using UnityEngine;

public class Optimizer : MonoBehaviour
{
    const float KILL_BEHIND_DIST = -20; //meters
    const float FAR_VISIBLE_DIST = 150; //meters

    public float distance = 0;
    Transform container;

    private void Start()
    {
        container = transform.GetChild(0); //should be container;
    }


    void Update()
    {
        KillBehind();
        SwitchAhead();
    }

    private void SwitchAhead()
    {
        if (container == null) return;
        bool b = (distance - Player.instance.distance <= FAR_VISIBLE_DIST) ? true : false;
        container.gameObject.SetActive(b);
    }

    private void KillBehind()
    {
        if (distance - Player.instance.distance <= KILL_BEHIND_DIST)
            Destroy(gameObject);
    }
}
