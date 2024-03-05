using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Landkarte")]
public class Landkarte
{
    public string Name { get; set; } = "nicht vergeben";

    public float Version { get; set; } = 0f;

    public string MinVersionARLandkarte { get; set; } = "0.0.0";

    public string CustomBackground { get; set; } = string.Empty;

    public List<Marker> Markers = new List<Marker>();

    public Landkarte() { }

    public Landkarte(string background, Marker m)
    {
        CustomBackground = background;
        Markers.Add(m);
    }
}
