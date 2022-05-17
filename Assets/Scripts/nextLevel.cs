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
            nextScene = "Beta Level";
        }
        else if (current.name == "Beta Level")
        {
            nextScene = "Final Level";
        }
        else if (current.name == "Final Level")
        {
            nextScene = "End of Game";
        }
    }
}
