using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 AnglePerSecond;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(AnglePerSecond * Time.deltaTime);
    }
}
