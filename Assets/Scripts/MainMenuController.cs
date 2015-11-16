using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void OnClickSinglePlayer()
    {
        Application.LoadLevel("Difficulty");
    }
    public void OnClickLeaerboard()
    {
        Application.LoadLevel("Leaderboard");
    }
    public void OnClickBack()
    {
        Application.LoadLevel("MainMenu");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
