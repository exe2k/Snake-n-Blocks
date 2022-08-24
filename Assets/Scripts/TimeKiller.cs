using UnityEngine;

public class TimeKiller : MonoBehaviour
{
    public float time = 3;

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0) Destroy(gameObject);
    }
}
