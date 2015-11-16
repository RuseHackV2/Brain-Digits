using UnityEngine;
using System.Collections;

public class DifficultyMenu : MonoBehaviour
{
    public void OnEasyClick()
    {
        Application.LoadLevel("NoobLevel1");
    }

    public void OnMedClick()
    {
        Application.LoadLevel("MediumLevel1");
    }

    public void OnHardClick()
    {
        Application.LoadLevel("HardLevel1");
    }

    public void OnInsClick()
    {
        Application.LoadLevel("InsaneLevel1");
    }

}
