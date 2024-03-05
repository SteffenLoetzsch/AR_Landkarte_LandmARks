using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker
{
    public string Picturename { get; set; } = string.Empty;
    public Information Info { get; set; } = new Information();

    public Marker() { }

    public Marker(string name, Information info)
    {
        Picturename = name;
        Info = info;
    }
    
}
