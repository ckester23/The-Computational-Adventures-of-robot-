using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    public DataHandler datah;
    float current_health;
    float delay = 1;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delta >= delay){
            current_health = datah.player_current_health;
            if (current_health <= 0){
                Debug.Log("Made it");
                SceneManager.LoadScene("Sasha-level");
            }
        }
        else {
            delta += Time.deltaTime;
        }
    }
}
