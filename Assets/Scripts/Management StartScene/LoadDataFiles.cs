using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadDataFiles : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TextMeshProUGUI chosenTextMessage;
    [SerializeField] private Button sceneLoaderButton;

    private GamePath path;
    private GameStorage storage;
    private XmlParser pa;
    private void Awake()
    {
        path = GameObject.FindGameObjectWithTag("Path").GetComponent<GamePath>();
        storage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameStorage>();
        pa = new XmlParser();
    }

    private void Start()
    {

        List<string> listOfNames = path.Ressources();

        foreach(string name in listOfNames)
        {
            GameObject newButton;
            newButton = Instantiate(buttonPrefab, content.transform, false);
            newButton.name = "newButton" + name;
            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;

            newButton.GetComponent<Button>().onClick.AddListener(() => {
                DirectoryInfo dir = path.ChooseDirectory(name);

                chosenTextMessage.text = $"Ausgewählte Karte: \n" + name;

                sceneLoaderButton.interactable = true;
            });
        }

        //pa.Save("C:/Users/Mavel/Desktop/AR-Images/Template2.xml");

    }

    
}
