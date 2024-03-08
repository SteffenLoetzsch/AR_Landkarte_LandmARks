using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    private GameStorage storage;

    [Header("Canvas Vertical Alignment")]
    [SerializeField] GameObject imageVertical;
    [SerializeField] GameObject ProjectInfoTextVertical;
    [SerializeField] TextMeshProUGUI projectNameTextVertical;
    [SerializeField] TextMeshProUGUI projectSubtitleTextVertical;
    [SerializeField] Button FurtherInfosButtonVertical;
    [SerializeField] GameObject ButtonsContainerVertical;
    [SerializeField] GameObject VideoButtonVertical;
    [SerializeField] GameObject WebInfoButtonVertical;

    [Header("Canvas Horizontal Alignment")]
    [SerializeField] GameObject imageHorizontal;
    [SerializeField] GameObject ProjectInfoTextHorizontal;
    [SerializeField] TextMeshProUGUI projectNameTextHorizontal;
    [SerializeField] TextMeshProUGUI projectSubtitleTextHorizontal;
    [SerializeField] Button FurtherInfosButtonHorizontal;
    [SerializeField] GameObject ButtonsContainerHorizontal;
    [SerializeField] GameObject VideoButtonHorizontal;
    [SerializeField] GameObject WebInfoButtonHorizontal;

    private Texture2D tex;

    private void Awake()
    {
        storage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameStorage>();

        if (storage.chosenImage != null)
        {
            var file = storage.chosenImage;
            byte[] fileData;

            fileData = File.ReadAllBytes(file.FullName);
            tex = new Texture2D(1, 1);
            tex.LoadImage(fileData);
        }
        imageVertical.GetComponent<RawImage>().texture = tex;
        imageHorizontal.GetComponent<RawImage>().texture = tex;

        ProjectInfoTextVertical.GetComponent<TextMeshProUGUI>().text = storage.chosenMarker.Info.InformationText;
        ProjectInfoTextHorizontal.GetComponent<TextMeshProUGUI>().text = storage.chosenMarker.Info.InformationText;

        projectNameTextVertical.text = storage.chosenMarker.Info.InformationTitle;
        projectNameTextHorizontal.text = storage.chosenMarker.Info.InformationTitle;

        projectSubtitleTextVertical.text = storage.chosenMarker.Info.AdditionalInfo;
        projectSubtitleTextHorizontal.text = storage.chosenMarker.Info.AdditionalInfo;

        if ((storage.chosenMarker.Info.WebsiteURL == string.Empty || storage.chosenMarker.Info.WebsiteURL == "string") && (storage.chosenMarker.Info.VideoFileLink == string.Empty || storage.chosenMarker.Info.VideoFileLink == "string"))
        {
            FurtherInfosButtonVertical.interactable = false;
            FurtherInfosButtonHorizontal.interactable = false;
        }
        else
        {
            FurtherInfosButtonVertical.onClick.AddListener(() =>
            {
                ProjectInfoTextVertical.SetActive(!ProjectInfoTextVertical.activeInHierarchy);
                ButtonsContainerVertical.SetActive(!ButtonsContainerVertical.activeInHierarchy);
            });

            FurtherInfosButtonHorizontal.onClick.AddListener(() =>
            {
                ProjectInfoTextHorizontal.SetActive(!ProjectInfoTextHorizontal.activeInHierarchy);
                ButtonsContainerHorizontal.SetActive(!ButtonsContainerHorizontal.activeInHierarchy);
            });

            if (storage.chosenMarker.Info.WebsiteURL == string.Empty || storage.chosenMarker.Info.WebsiteURL == "string")
            {
                WebInfoButtonVertical.SetActive(false);
                WebInfoButtonHorizontal.SetActive(false);
            }
            else 
            {
                WebInfoButtonVertical.GetComponent<Button>().onClick.AddListener(() => 
                {
                    Application.OpenURL(storage.chosenMarker.Info.WebsiteURL);
                });

                WebInfoButtonHorizontal.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Application.OpenURL(storage.chosenMarker.Info.WebsiteURL);
                });
            }

            if (storage.chosenMarker.Info.VideoFileLink == string.Empty || storage.chosenMarker.Info.VideoFileLink == "string")
            {
                VideoButtonVertical.SetActive(false);
                VideoButtonHorizontal.SetActive(false);
            }
            else
            {
                VideoButtonVertical.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Application.OpenURL(storage.chosenMarker.Info.VideoFileLink);
                });

                VideoButtonHorizontal.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Application.OpenURL(storage.chosenMarker.Info.VideoFileLink);
                });
            }
        }

    }


}
