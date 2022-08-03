using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] GameObject controlMenu;


    public void pauseGame()
    {
        controlMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        controlMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
