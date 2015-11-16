using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text money;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetString("Name", "Unknown") != "")
        {
            Highscores.AddNewHighscore(PlayerPrefs.GetString("Name","Unknown"), PlayerPrefs.GetInt("Total"));
        }

        money.text = "Your score is: " + PlayerPrefs.GetInt("Total") + " Points";
    }

    public void back()
    {
        Application.LoadLevel("Menu");
    }


}
