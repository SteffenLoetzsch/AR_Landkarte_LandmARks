using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadDataFiles : MonoBehaviour
{
    [SerializeField] private GameObject contentVertical;
    [SerializeField] private GameObject contentHorizontal;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TextMeshProUGUI chosenTextMessageVertical;
    [SerializeField] private TextMeshProUGUI chosenTextMessageHorizontal;
    [SerializeField] private Button sceneLoaderButtonVertical;
    [SerializeField] private Button sceneLoaderButtonHorizontal;

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
            GameObject newButtonVertical;
            GameObject newButtonHorizontal;
            newButtonVertical = Instantiate(buttonPrefab, contentVertical.transform, false);
            newButtonVertical.name = "newButton" + name;
            newButtonVertical.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;

            newButtonHorizontal = Instantiate(newButtonVertical, contentHorizontal.transform, false);

            newButtonVertical.GetComponent<Button>().onClick.AddListener(() => {
                DirectoryInfo dir = path.ChooseDirectory(name);

                chosenTextMessageVertical.text = $"Ausgewählte Karte: \n" + name;

                sceneLoaderButtonVertical.interactable = true;
            });

            newButtonHorizontal.GetComponent<Button>().onClick.AddListener(() => {
                DirectoryInfo dir = path.ChooseDirectory(name);

                chosenTextMessageHorizontal.text = $"Ausgewählte Karte: \n" + name;

                sceneLoaderButtonHorizontal.interactable = true;
            });
        }

        //pa.Save("C:/Users/Mavel/Desktop/AR-Images/Template2.xml");

    }

    
}
