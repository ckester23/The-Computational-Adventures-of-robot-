using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{

    public int player_health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (player_health <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

}
