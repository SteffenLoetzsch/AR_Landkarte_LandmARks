using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class GameStorage : MonoBehaviour
{
    private GamePath gamePath { get; set; }
    public MutableRuntimeReferenceImageLibrary library { get; set; }
    public bool FirstRun { get; set; } = true;
    public string ImageChosenName { get; set; }
    public FileInfo chosenImage { get; set; }
    public bool HasCustomBackground { get;  set; }
    public Texture2D CustomBackground;
    public bool DirectlyDisplayText { get;  set; }
    public IEnumerable<Marker> markers { get; set; }
    public Marker chosenMarker { get; set; }

    private void Start()
    {
            gamePath = GameObject.FindGameObjectWithTag("Path").GetComponent<GamePath>();
    }

    public Marker SetChosenMarker(string imageName)
    {
        var marker = from mark in markers where mark.Picturename == imageName select mark;

        chosenMarker = marker.FirstOrDefault();
        return chosenMarker;
    }
}
