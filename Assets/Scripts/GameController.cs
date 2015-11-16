using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;

public class GameController : MonoBehaviour
{
    public GameObject[] circles;
    public GameObject[] progressBar;
    public GameObject jockerGameObject;
    public GameObject jockerButton;
    public Text hintText;
    public Sprite blueCircle;
    public Sprite redCircle;
    public Sprite greenCircle;
    public Text redText;
    public Text greenText;
    public Text[] circlePoints;
    public Text timerText;
    public Text earnedPoints;
    public Text totalPoints;
    public GameHardnes gameHardnes;
    public int levelNow;
    public int hintPoints; // TODO
    public Text brainPointsText;
    public GameObject winPanel;
    private float timer;
    private int[] counter;
    private int redSum;
    private int greenSum;
    private int brainPoints;
    private int brainPointsSubstract;
    private bool isGameFinished;
    // Use this for initialization

    private void Start()
    {
        counter = new int[9];
        redSum = 0;
        greenSum = 0;
        timer = 0;
        HardnesSelect();
        brainPointsText.text = brainPoints.ToString();
        Invoke("SubstractPoints", 10f);
        winPanel.SetActive(false);
        PlayerPrefs.GetInt("Total", 0);
        PlayerPrefs.GetInt("Level" + levelNow.ToString(), 0);

        if (gameHardnes == GameHardnes.Easy)
        {
            PlayerPrefs.GetInt("NoobLevel" + levelNow, 0);
        }
        else if (gameHardnes == GameHardnes.Medium)
        {
            PlayerPrefs.GetInt("MediumLevel" + levelNow, 0);
        }
        else if (gameHardnes == GameHardnes.Hard)
        {
            PlayerPrefs.GetInt("HardLevel" + levelNow, 0);
        }
        else if (gameHardnes == GameHardnes.Insane)
        {
            PlayerPrefs.GetInt("InsaneLevel" + levelNow, 0);
        }

        isGameFinished = false;
        for (int i = levelNow - 1; i < progressBar.Length; i++)
        {
            progressBar[i].SetActive(false);
        }
        jockerGameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGameFinished == false)
        {
            UpdateTimer();
        }

    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        TimeSpan span = new TimeSpan(0, 0, (int)timer);
        StringBuilder sb = new StringBuilder();
        sb.Append(span.ToString()[3]);
        sb.Append(span.ToString()[4]);
        sb.Append(span.ToString()[5]);
        sb.Append(span.ToString()[6]);
        sb.Append(span.ToString()[7]);
        timerText.text = sb.ToString();
    }

    private void SubstractPoints()
    {
        brainPoints -= brainPointsSubstract;

        if (brainPoints < 0)
        {
            brainPoints = 0;
        }

        brainPointsText.text = brainPoints.ToString();
        Invoke("SubstractPoints", 10f);
    }

    private void HardnesSelect()
    {
        if (gameHardnes == GameHardnes.Easy)
        {
            brainPoints = 100;
            brainPointsSubstract = 10;
        }
        else if (gameHardnes == GameHardnes.Medium)
        {
            brainPoints = 500;
            brainPointsSubstract = 50;
        }
        else if (gameHardnes == GameHardnes.Hard)
        {
            brainPoints = 2000;
            brainPointsSubstract = 100;
        }
        else if (gameHardnes == GameHardnes.Insane)
        {
            brainPoints = 5000;
            brainPointsSubstract = 250;
        }
    }

    private void OnClickCircle(int count)
    {
        counter[count]++;
        if (counter[count] > 2)
        {
            counter[count] = 0;
        }

        if (counter[count] == 0)
        {
            circles[count].GetComponent<Image>().sprite = blueCircle;
            greenSum -= int.Parse(circlePoints[count].text);
        }
        else if (counter[count] == 1)
        {
            circles[count].GetComponent<Image>().sprite = redCircle;
            redSum += int.Parse(circlePoints[count].text);
        }
        else
        {
            circles[count].GetComponent<Image>().sprite = greenCircle;
            redSum -= int.Parse(circlePoints[count].text);
            greenSum += int.Parse(circlePoints[count].text);
        }
        greenText.text = greenSum.ToString();
        redText.text = redSum.ToString();

        if (greenSum == redSum && greenSum != 0 && isGameFinished == false)
        {
            brainPointsSubstract = 0;
            winPanel.SetActive(true);
            isGameFinished = true;
            if (gameHardnes == GameHardnes.Easy)
            {
                if (PlayerPrefs.GetInt("NoobLevel" + levelNow.ToString(), 0) < brainPoints)
                {
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") - (PlayerPrefs.GetInt("EasyLevel" + levelNow.ToString())));
                    PlayerPrefs.SetInt("EasyLevel" + levelNow.ToString(), brainPoints);
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") + brainPoints);
                }
            }
            else if (gameHardnes == GameHardnes.Medium)
            {
                if (PlayerPrefs.GetInt("MediumLevel" + levelNow.ToString(), 0) < brainPoints)
                {
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") - (PlayerPrefs.GetInt("MediumLevel" + levelNow.ToString())));
                    PlayerPrefs.SetInt("MediumLevel" + levelNow.ToString(), brainPoints);
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") + brainPoints);
                }
            }
            else if (gameHardnes == GameHardnes.Hard)
            {
                if (PlayerPrefs.GetInt("MediumLevel" + levelNow.ToString(), 0) < brainPoints)
                {
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") - (PlayerPrefs.GetInt("MediumLevel" + levelNow.ToString())));
                    PlayerPrefs.SetInt("MediumLevel" + levelNow.ToString(), brainPoints);
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") + brainPoints);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("InsaneLevel" + levelNow.ToString(), 0) < brainPoints)
                {
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") - (PlayerPrefs.GetInt("InsaneLevel" + levelNow.ToString())));
                    PlayerPrefs.SetInt("InsaneLevel" + levelNow.ToString(), brainPoints);
                    PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") + brainPoints);
                }
            }


            earnedPoints.text = "EARNED: " + brainPoints.ToString() + " Points";
            totalPoints.text = "TOTAL: " + PlayerPrefs.GetInt("Total").ToString() + " Points";
        }

    }

    public void ZeroOnClick()
    {
        OnClickCircle(0);
    }

    public void OneOnClick()
    {
        OnClickCircle(1);
    }

    public void TwoOnClick()
    {
        OnClickCircle(2);
    }

    public void ThreeOnClick()
    {
        OnClickCircle(3);
    }

    public void FourOnClick()
    {
        OnClickCircle(4);
    }

    public void FiveOnClick()
    {
        OnClickCircle(5);
    }
    public void SixOnClick()
    {
        OnClickCircle(6);
    }
    public void SevenOnClick()
    {
        OnClickCircle(7);
    }
    public void EightOnClick()
    {
        OnClickCircle(8);
    }

    public void NextLevel()
    {

        if (gameHardnes == GameHardnes.Easy)
        {
            if (levelNow + 1 > 3)
            {
                Application.LoadLevel("Difficulty");
            }
            Application.LoadLevel("NoobLevel" + (levelNow + 1));
        }
        else if (gameHardnes == GameHardnes.Medium)
        {
            if (levelNow + 1 > 3)
            {
                Application.LoadLevel("Difficulty");
            }
            Application.LoadLevel("MediumLevel" + (levelNow + 1));
        }
        else if (gameHardnes == GameHardnes.Hard)
        {
            if (levelNow + 1 > 1)
            {
                Application.LoadLevel("Difficulty");
            }
            Application.LoadLevel("HardLevel" + (levelNow + 1));
        }
        else if (gameHardnes == GameHardnes.Insane)
        {
            if (levelNow + 1 > 1)
            {
                Application.LoadLevel("Difficulty");
            }
            Application.LoadLevel("InsaneLevel" + (levelNow + 1));
        }

    }

    public void HintClick()
    {
        jockerGameObject.SetActive(true);
        jockerButton.SetActive(false);
        brainPoints /= 2;
        brainPointsText.text = brainPoints.ToString();
        hintText.text = "The sum is: " + hintPoints;

    }

    public void HintCloseClick()
    {
        jockerGameObject.SetActive(false);
    }

    public void MenuClicked()
    {
        Application.LoadLevel("Difficulty");
    }

    public void SkipOnClick()
    {
        try
        {
            if (gameHardnes == GameHardnes.Easy)
            {
                if (levelNow + 1 > 3)
                {
                    Application.LoadLevel("Difficulty");
                }
                Application.LoadLevel("NoobLevel" + (levelNow + 1));
            }
            else if (gameHardnes == GameHardnes.Medium)
            {
                if (levelNow + 1 > 3)
                {
                    Application.LoadLevel("Difficulty");
                }
                Application.LoadLevel("MediumLevel" + (levelNow + 1));
            }
            else if (gameHardnes == GameHardnes.Hard)
            {
                if (levelNow + 1 > 1)
                {
                    Application.LoadLevel("Difficulty");
                }
                Application.LoadLevel("HardLevel" + (levelNow + 1));
            }
            else if (gameHardnes == GameHardnes.Insane)
            {
                if (levelNow + 1 > 1)
                {
                    Application.LoadLevel("Difficulty");
                }
                Application.LoadLevel("InsaneLevel" + (levelNow + 1));
            }
        }
        catch (Exception)
        {

            Application.LoadLevel("Difficulty");
        }
    }



}
