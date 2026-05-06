using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannel;

    [SerializeField]
    private SceneSO[] level_1Scenes;
    [SerializeField]
    private SceneSO[] level_2Scenes;
    [SerializeField]
    private SceneSO[] level_3Scenes;


    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject levelsPanel;
    [SerializeField]
    private GameObject controlsPanel;

    public void StartLevel_1()
    {
        sceneLoaderChannel.RaiseEvent(level_1Scenes);
    }

    public void StartLevel_2()
    {
        sceneLoaderChannel.RaiseEvent(level_2Scenes);
    }

    public void StartLevel_3()
    {
        sceneLoaderChannel.RaiseEvent(level_3Scenes);
    }

    public void ShowLevels()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        levelsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}