using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
