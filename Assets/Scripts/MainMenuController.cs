using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Alpha Level");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
