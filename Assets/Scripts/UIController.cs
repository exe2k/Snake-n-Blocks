using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class UIController : MonoBehaviour
{
    [SerializeField] GameManager GM;

    [SerializeField] RectTransform startUI, gameUI, loseUI, winUI;
    RectTransform[] allScreens;

    [Header("Audio")]
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    AudioSource _as;

    private void Start()
    {
        _as = GetComponent<AudioSource>();

        allScreens = new RectTransform[] { startUI, gameUI, loseUI, winUI };
        GM.OnStateChanged.AddListener(SwitchScreen);
        Player.onPointsChanged.AddListener(UpdateGameScreen);

        ShowStart();
    }

    private void UpdateGameScreen(int points)
    {
        gameUI.Find("Scores").GetComponent<TextMeshProUGUI>().text = points + "";
    }

    public void SwitchScreen(GameManager.GameStates state)
    {
        switch (state)
        {
            case GameManager.GameStates.start:
                ShowStart();
                break;

            case GameManager.GameStates.game:
                ShowGame();
                break;

            case GameManager.GameStates.win:
                ShowWin();
                break;

            case GameManager.GameStates.lose:
                ShowLose();
                break;
        }
    }

    void ShowStart()
    {
        SwitchOffAll();
        startUI.gameObject.SetActive(true);
    }

    void ShowGame()
    {
        SwitchOffAll();
        gameUI.gameObject.SetActive(true);
        UpdateGameScreen(0);
        gameUI.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "LEVEL "+GM.level ;
    }

    void ShowLose()
    {
        SwitchOffAll();
        loseUI.gameObject.SetActive(true);
        _as?.PlayOneShot(loseSound);
    }

    void ShowWin()
    {
        SwitchOffAll();
        winUI.gameObject.SetActive(true);
        winUI.Find("Points").GetComponent<TextMeshProUGUI>().text = $"{Player.instance.points}";
        _as?.PlayOneShot(winSound);
    }

    void SwitchOffAll()
    {
        foreach (var screen in allScreens)
        {
            screen.gameObject.SetActive(false);
        }
    }
}
