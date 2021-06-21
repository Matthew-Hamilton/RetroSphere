using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderzCamera : MonoBehaviour
{
    Camera myCamera;

    float startSize = 5;
    float gameSize = 15;

    float targetSize;
    [Range(1, 10)]
    int sizeChangeSpeed = 5;

    bool loaded = false;


    // Start is called before the first frame update
    void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!loaded)
        {
            targetSize = gameSize;
        }

        if(Mathf.Abs(myCamera.orthographicSize - targetSize) > 0.1)
        {
            myCamera.orthographicSize -= myCamera.orthographicSize.CompareTo(targetSize) * sizeChangeSpeed * Time.deltaTime;
        }
        else
        {
            loaded = true;
            myCamera.orthographicSize = targetSize;
        }
    }
}
