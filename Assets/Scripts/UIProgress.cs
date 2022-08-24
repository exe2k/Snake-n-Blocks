using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class UIProgress : MonoBehaviour
{
    Scrollbar scrollbar;
    float distance = 0;


    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        InvokeRepeating("StartCheckProgress", 0, .33f);
    }

    private void StartCheckProgress()
    {
        if (Player.instance == null) return;
        
        distance = Player.instance.distance / Player.instance.fullDistance *1.02f;
        scrollbar.size = distance;
    }
}
