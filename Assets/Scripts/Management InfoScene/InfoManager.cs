using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    private GamePath path;
    private GameStorage storage;

    [SerializeField] GameObject image;
    [SerializeField] GameObject ProjectInfoText;
    [SerializeField] GameObject scrollview;
    [SerializeField] TextMeshProUGUI projectNameText;
    [SerializeField] TextMeshProUGUI projectSubtitleText;
    [SerializeField] Button FurtherInfosButton;
    [SerializeField] GameObject ButtonsContainer;
    [SerializeField] GameObject VideoButton;
    [SerializeField] GameObject WebInfoButton;


    private Texture2D tex;

    private void Awake()
    {
        path = GameObject.FindGameObjectWithTag("Path").GetComponent<GamePath>();
        storage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameStorage>();

        if (storage.chosenImage != null)
        {
            var file = storage.chosenImage;
            byte[] fileData;

            fileData = File.ReadAllBytes(file.FullName);
            tex = new Texture2D(1, 1);
            tex.LoadImage(fileData);
        }
        image.GetComponent<RawImage>().texture = tex;
        ProjectInfoText.GetComponent<TextMeshProUGUI>().text = storage.chosenMarker.Info.InformationText;
        projectNameText.text = storage.chosenMarker.Info.InformationTitle;
        projectSubtitleText.text = storage.chosenMarker.Info.AdditionalInfo;

        if ((storage.chosenMarker.Info.WebsiteURL == string.Empty || storage.chosenMarker.Info.WebsiteURL == "string") && (storage.chosenMarker.Info.VideoFileLink == string.Empty || storage.chosenMarker.Info.VideoFileLink == "string"))
        {
            FurtherInfosButton.interactable = false;
        }
        else
        {
            FurtherInfosButton.onClick.AddListener(() =>
            {
                ProjectInfoText.SetActive(!ProjectInfoText.activeInHierarchy);
                ButtonsContainer.SetActive(!ButtonsContainer.activeInHierarchy);
            });

            if(storage.chosenMarker.Info.WebsiteURL == string.Empty || storage.chosenMarker.Info.WebsiteURL == "string")
            {
                WebInfoButton.SetActive(false);
            }
            else 
            {
                WebInfoButton.GetComponent<Button>().onClick.AddListener(() => 
                {
                    Application.OpenURL(storage.chosenMarker.Info.WebsiteURL);
                }); 
            }

            if (storage.chosenMarker.Info.VideoFileLink == string.Empty || storage.chosenMarker.Info.VideoFileLink == "string")
            {
                VideoButton.SetActive(false);
            }
            else
            {
                VideoButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Application.OpenURL(storage.chosenMarker.Info.VideoFileLink);
                });
            }
        }

    }


}
