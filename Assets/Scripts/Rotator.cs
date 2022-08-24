using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 AnglePerSecond;

    [SerializeField]bool isDependOnPlayer = false;

    void Update()
    {
        if (isDependOnPlayer && !Player.instance.isControlsOn) return;
        transform.rotation *= Quaternion.Euler(AnglePerSecond * Time.deltaTime);
    }
}
