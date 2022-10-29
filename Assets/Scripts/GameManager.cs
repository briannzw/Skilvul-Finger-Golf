using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public PlayerController player;
    public Hole hole;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (hole == null) return;

        if(hole.entered && !gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Finished!\nShoot Count : " + player.shootCount;
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
    }
}
