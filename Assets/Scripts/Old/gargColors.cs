using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gargColors : MonoBehaviour
{
    public Material light_cone;

    GameObject[] gargs;
    GameObject garg;
    GameObject garg1;
    GameObject garg2;

    GameObject john;

    float dist;
    float dist1;
    float dist2;
    float closest;
    float t;

    Color lerpedColor;

    // Start is called before the first frame update
    void Start()
    {
        
        light_cone.EnableKeyword("_EMISSION");
        light_cone.SetColor("_EmissionColor", Color.green);

        john = GameObject.Find("JohnLemon");

        gargs = GameObject.FindGameObjectsWithTag("Gargoyle");
        garg = gargs[0];
        garg1 = gargs[1];
        garg2 = gargs[2];
        
        dist = 0f;
        dist1 = 0f;
        dist2 = 0f;

        lerpedColor = Color.green;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool gargsGood = !(garg is null) & !(garg1 is null) & !(garg2 is null);
        // May replace these with own DistanceSquared function...
        if (john is not null & gargsGood)
        {
            dist = Vector3.Distance(john.transform.position, garg.transform.position);
            dist1 = Vector3.Distance(john.transform.position, garg1.transform.position);
            dist2 = Vector3.Distance(john.transform.position, garg2.transform.position);

            closest = Mathf.Min(dist, dist1, dist2);

            ChangeFlashColor(light_cone, closest);
        }
    }
    void ChangeFlashColor(Material flash, float dist)
    {
        /* dist = 5, we want total green
         *  1 - ((5-5) / 2.5) = 1 - (0/2.5) = 1 - 0 = 1
         *  
         * dist = 2.5, we want total red
         *  1 - ((5-2) / 2.5) = 1 - (3/2.5) = 1 - 1 = 0
         */

        t = 1f - ((5f - dist) / 2.5f);

        if (t < 0) { t = 0; }
        else if (t > 1) { t = 1; }

        DoLerping(Color.red, Color.green, t);
        flash.SetColor("_EmissionColor", lerpedColor);
    }
    void DoLerping(Color color1, Color color2, float t_val)
    {
        lerpedColor = Color.Lerp(color1, color2, t_val);
    }

}
