using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlParser : Parser
{
    private string customBackgroundName;

    public override string Extension { get; protected set; }
    public override bool HasCustomBackground { get; protected set; } = false;
    public override string CustomBackground
    {
        get => customBackgroundName;
        protected set
        {
            customBackgroundName = value;
            HasCustomBackground = customBackgroundName != string.Empty;
        }
    }
    public override bool DirectlyDisplayText { get; protected set; } = false;
    public override IEnumerable<Marker> Markers { get; protected set; }
    private string inTextNewLineCharacter;
    private string greeting;
    private string emergencyMarkerName = string.Empty;

    public override string InTextNewLineCharacter
    {
        get => inTextNewLineCharacter;
        protected set => inTextNewLineCharacter = value;
    }
    public XmlParser()
    {
        Extension = ".xml";
        InTextNewLineCharacter = "<br>";
    }

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(Landkarte));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, new Landkarte("string", new Marker("Name", new Information("string","string", "string","string","string"))));
        stream.Close();
    }

    public override bool Parse(GamePath path)
    {

        try
        {
            List<Marker> markers = new List<Marker>();
            XmlSerializer serializer = new XmlSerializer(typeof(Landkarte));
            TextReader reader = new StreamReader(path.XmlFile().FullName);
            Landkarte karte = serializer.Deserialize(reader) as Landkarte;


            markers = karte.Markers;

            Markers = markers;

            return true;
        }
        catch (Exception)
        {
            return false;
        }


    }
    public override bool IsCompatible(GamePath path)
    {
        return path.XmlFile().Extension == Extension;
    }
}

