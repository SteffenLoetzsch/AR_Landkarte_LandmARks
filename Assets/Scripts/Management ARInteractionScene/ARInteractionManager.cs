using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager manager;

    private GamePath path;
    private GameStorage storage;

    [SerializeField] GameObject trackerObjectPrefab;
    [SerializeField] GameObject UIPrefab;
    [SerializeField] GameObject contentVertical;
    [SerializeField] GameObject contentHorizontal;
    private readonly Dictionary<string, GameObject> _instantiatedPrefabsVertical = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, GameObject> _instantiatedPrefabsHorizontal = new Dictionary<string, GameObject>();
    private List<FileInfo> imagesList;

    private void Awake()
    {
        path = GameObject.FindGameObjectWithTag("Path").GetComponent<GamePath>();
        storage = GameObject.FindGameObjectWithTag("Storage").GetComponent<GameStorage>();
    }

    private void Start()
    {
        if (storage.FirstRun)
        {
            var lib = manager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;
            storage.library = lib;
            storage.FirstRun = false;
        }
        
        manager.referenceLibrary = storage.library;
        manager.enabled = true;

        imagesList = path.ImageList();

        foreach (var image in imagesList)
        {

            Texture2D imageTexture = null;
            byte[] fileData;
            var name = image.Name.Split(".")[0];

            fileData = File.ReadAllBytes(image.FullName);
            imageTexture = new Texture2D(2, 2);
            imageTexture.LoadImage(fileData);

            if (manager.referenceLibrary is MutableRuntimeReferenceImageLibrary library)
            {
                StartCoroutine(AddImageJob(imageTexture, name, library));
            }
        }
    }

    private IEnumerator AddImageJob(Texture2D texture, string name, MutableRuntimeReferenceImageLibrary lib)
    {
        yield return null;
        if (lib.IsTextureFormatSupported(texture.format))
        {
            var state = lib.ScheduleAddImageWithValidationJob(texture, name, 0.1f);
            
        }

        else Debug.LogError("The image " + name + " has a format that is not supported.");

        yield return new WaitForSeconds(1);
    }

    void OnEnable() => manager.trackedImagesChanged += OnTrackedImagesChanged;

    void OnDisable() => manager.trackedImagesChanged -= OnTrackedImagesChanged;

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (!_instantiatedPrefabsVertical.ContainsKey(trackedImage.referenceImage.name))
            {
                //GameObject newPrefab = Instantiate(trackerObjectPrefab, trackedImage.transform);
                GameObject newUIPrefabVertical = Instantiate(UIPrefab, contentVertical.transform);
                GameObject newUIPrefabHorizontal = Instantiate(UIPrefab, contentHorizontal.transform);


                foreach (var i in storage.markers)
                {
                    if (i.Picturename == trackedImage.referenceImage.name)
                    {
                        //newPrefab.transform.Find("ImageTextCanvas").Find("ImageText").GetComponent<TextMeshProUGUI>().text = i.Info.InformationTitle;
                        newUIPrefabVertical.transform.Find("ImageText").GetComponent<TextMeshProUGUI>().text = i.Info.InformationTitle;
                        newUIPrefabHorizontal.transform.Find("ImageText").GetComponent<TextMeshProUGUI>().text = i.Info.InformationTitle;
                        break;
                    }
                }

                storage.ImageChosenName = trackedImage.referenceImage.name;
                var imagefile = imagesList.Where(file => file.Name.Split(".")[0] == trackedImage.referenceImage.name);
                storage.chosenImage = imagefile.FirstOrDefault();
                storage.chosenMarker = storage.SetChosenMarker(imagefile.FirstOrDefault().Name.Split(".")[0]);

                _instantiatedPrefabsVertical.Add(trackedImage.referenceImage.name, newUIPrefabVertical);
                _instantiatedPrefabsVertical[trackedImage.referenceImage.name].transform.Find("ChooseImageButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    storage.ImageChosenName = trackedImage.referenceImage.name;
                    var imagefile = imagesList.Where(file => file.Name.Split(".")[0] == trackedImage.referenceImage.name);
                    storage.chosenImage = imagefile.FirstOrDefault();
                    storage.chosenMarker = storage.SetChosenMarker(imagefile.FirstOrDefault().Name.Split(".")[0]);
                    SceneManager.LoadScene("InfoScene");
                });

                _instantiatedPrefabsHorizontal.Add(trackedImage.referenceImage.name, newUIPrefabHorizontal);
                _instantiatedPrefabsHorizontal[trackedImage.referenceImage.name].transform.Find("ChooseImageButton").GetComponent<Button>().onClick.AddListener(() =>
                {
                    storage.ImageChosenName = trackedImage.referenceImage.name;
                    var imagefile = imagesList.Where(file => file.Name.Split(".")[0] == trackedImage.referenceImage.name);
                    storage.chosenImage = imagefile.FirstOrDefault();
                    storage.chosenMarker = storage.SetChosenMarker(imagefile.FirstOrDefault().Name.Split(".")[0]);
                    SceneManager.LoadScene("InfoScene");
                });
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        { 
            _instantiatedPrefabsVertical[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
            _instantiatedPrefabsHorizontal[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            //_instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            //Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
        }
    }
}
