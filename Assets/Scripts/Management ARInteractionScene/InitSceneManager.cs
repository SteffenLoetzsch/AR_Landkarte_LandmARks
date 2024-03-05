using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InitSceneManager : MonoBehaviour
{
    private GameStorage storage;
    private GamePath path;
    private List<Parser> parsers = new();

    private void Awake()
    {
        storage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameStorage>();
        path = GameObject.FindGameObjectWithTag("Path").GetComponent<GamePath>();

    }

    private void Start()
    {
        parsers.Add(new XmlParser());

        foreach(var parser in parsers)
        {
            if (parser.IsCompatible(path))
            {
                if (parser.Parse(path))
                {
                    if (parser.HasCustomBackground)
                    {
                        // Change Background.
                    }
                    storage.DirectlyDisplayText = parser.DirectlyDisplayText;
                    storage.markers = parser.Markers;
                    
                }
            }
        }
    } 
}


