using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannel;

    [SerializeField]
    private LevelManagerSO level_1_ManagerSO;
    [SerializeField]
    private LevelManagerSO level_2_ManagerSO;
    [SerializeField]
    private LevelManagerSO level_3_ManagerSO;


    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject levelsPanel;
    [SerializeField]
    private GameObject controlsPanel;
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private PlayerInfo playerInfo;

    void OnEnable()
    {
        if(playerInfo.player != null && playerInfo.player.Health <= 0f)
        {
            ShowGameOver();
        }
        else
        {
            ShowMenu();
        }
    }

    public void StartLevel_1()
    {
        sceneLoaderChannel.RaiseEvent(level_1_ManagerSO.scenesToLoad);
    }

    public void StartLevel_2()
    {
        sceneLoaderChannel.RaiseEvent(level_2_ManagerSO.scenesToLoad);
    }

    public void StartLevel_3()
    {
        sceneLoaderChannel.RaiseEvent(level_3_ManagerSO.scenesToLoad);
    }

    public void ShowLevels()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        levelsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowControls()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}