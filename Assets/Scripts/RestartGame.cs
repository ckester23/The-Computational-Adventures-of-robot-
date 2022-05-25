using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    public DataHandler datah;
    public GameObject player;
    public PlayerCharacterAbilities player_stats;

    float current_health;
    float delay = 1;
    float delta = 0;

    Scene currentScene;

    Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        startPoint = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delta >= delay){
            current_health = datah.player_current_health;
            if (current_health <= 0){
                // if we have passed a checkpoint
                if (datah.respawnPoint != startPoint)
                {
                    // reset player position and health
                    Debug.Log(datah.player_current_health);
                    player_stats.player_currenthealth = 3;
                    player_stats.player_energy = 0.0f;
                    player_stats.player_tools = 0;
                    player.transform.position = datah.respawnPoint;
                }
                else
                {
                    SceneManager.LoadScene(currentScene.name);
                }
            }
        }
        else {
            delta += Time.deltaTime;
        }
    }
}
