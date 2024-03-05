using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var n = cam.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(n);
    }
}
