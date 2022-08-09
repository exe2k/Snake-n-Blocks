using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMover : MonoBehaviour
{
    public Vector3 direction, rotation;

    void Update()
    {
        transform.localPosition += direction   * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotation* Time.deltaTime);
    }
}
