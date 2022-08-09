using System;
using UnityEngine;

/// <summary>Player Main Class</summary>

[
    RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(Collider)),
]


public class Player : MonoBehaviour
{
    public static Player instance;
    public bool isControlsOn = true;
    [SerializeField] float speed = 20f;

    private Rigidbody rb;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        rb = GetComponent<Rigidbody>();
    }




    void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;

        //Controls();
    }

    private void Controls()
    {
        if (Input.GetKey(KeyCode.A))
        {

        }
    }
}
