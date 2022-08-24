using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Progress : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    float distance = 0;


    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        InvokeRepeating("StartCheckProgress", 0, .33f);
    }

    private void StartCheckProgress()
    {
        if (Player.instance == null) return;
        
        distance = Player.instance.distance / Player.instance.fullDistance *1.05f;
        scrollbar.size = distance;
    }
}
