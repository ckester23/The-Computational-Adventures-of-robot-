using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class nextLevel : MonoBehaviour
{
    public Collider player;

    Scene currentScene;
    string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        determineNextScene(currentScene);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Made it");
            SceneManager.LoadScene(nextScene);
        }
    }

    void determineNextScene(Scene current)
    {
        if (current.name == "Alpha Level")
        {
            nextScene = "Level 2";
        }
        else if (current.name == "Level 2")
        {
            nextScene = "Level 3";
        }
        else if (current.name == "Level 3")
        {
            nextScene = "End of Game";
        }
        else if (current.name == "End of Game")
        {
            nextScene = "MainMenu";
        }
    }
}
