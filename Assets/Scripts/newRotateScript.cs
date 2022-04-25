using System.Collections;
using UnityEngine;

public class newRotateScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 0) * Time.deltaTime);
    }
}
