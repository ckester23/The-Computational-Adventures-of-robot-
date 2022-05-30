using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceAudioHnadler : MonoBehaviour
{
    public GameObject player;
    public AudioSource objectAudio;


    private float dist_Player;

    // Start is called before the first frame update
    void Start()
    {
        dist_Player = Vector3.Distance(transform.position, player.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist_Player = Vector3.Distance(transform.position, player.transform.position);

        if (dist_Player <= 10f)
        {
            objectAudio.Play();
        }
        else
        {
            objectAudio.Stop();
        }
    }
}
