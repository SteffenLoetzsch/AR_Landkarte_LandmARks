using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GamePath : MonoBehaviour
{
    public string PathToGamesFolder { get; protected set; } = string.Empty;
    public string PathToMapFolder { get; protected set; } = string.Empty;
    public DirectoryInfo[] mapFolders;
    public DirectoryInfo pathChosen;

    private void Awake()
    {
        PathToGamesFolder = Application.persistentDataPath + "/MapAssets";

        if (!Directory.Exists(PathToGamesFolder))
            Directory.CreateDirectory(PathToGamesFolder);

        DirectoryInfo dir = new DirectoryInfo(PathToGamesFolder);
        mapFolders = dir.GetDirectories();
    }



    public List<string> Ressources()
    /* Gives the name of each directory as a string, compiled in a list. */
    {

        List<string> namesList = new List<string>();

        foreach (DirectoryInfo f in mapFolders)
        {
            namesList.Add(f.Name);
        }

        return namesList;
    }

    public DirectoryInfo ChooseDirectory(string DirectoryName)
    /* Selects the Map used for this iteration of the game.*/
    {
        foreach (DirectoryInfo f in mapFolders)
        {
            if (f.Name == DirectoryName)
            {
                PathToMapFolder = f.FullName;
                pathChosen = f;
                return f;
            }
        }
        return null;
    }

    public List<FileInfo> ImageList()
    {
        List<FileInfo> filesList = new List<FileInfo>();

        var files = pathChosen.GetFiles();
        foreach(var file in files)
        {
            if (!file.Name.ToLower().Contains(".xml".ToLower()))
            {
                filesList.Add(file);
            }
        }

        return filesList;
    }
    public FileInfo XmlFile()
    {
        List<FileInfo> xmlList = new List<FileInfo>();

        var files = pathChosen.GetFiles();
        foreach (var file in files)
        {
            if (file.Name.ToLower().Contains(".xml".ToLower()))
            {
                xmlList.Add(file);
            }
        }

        return xmlList[0];
    }
}
