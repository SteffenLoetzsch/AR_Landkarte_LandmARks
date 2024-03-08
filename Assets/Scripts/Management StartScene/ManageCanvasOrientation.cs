using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCanvasOrientation : MonoBehaviour
{
    [SerializeField] GameObject vertical;
    [SerializeField] GameObject horizontal;

    // Start is called before the first frame update
    void Start()
    {
       if(Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            vertical.SetActive(true);
            horizontal.SetActive(false);
        }

        else
        {
            vertical.SetActive(false);
            horizontal.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            vertical.SetActive(true);
            horizontal.SetActive(false);
        }

        else if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            vertical.SetActive(false);
            horizontal.SetActive(true);
        }
    }
}
