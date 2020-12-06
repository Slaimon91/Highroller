using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Awake()
    {
        int GameStatusCount = FindObjectsOfType<LevelLoader>().Length;
        if (GameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadOverworldScene(string key)
    {
        FindObjectOfType<PlayerControlsManager>().ChangeToOverworld();
        SceneManager.LoadScene(key);
        StartCoroutine(LoadOWSceneDelay());
    }

    public void LoadOverworldSceneFromMenu(string key)
    {
        FindObjectOfType<PlayerControlsManager>().ChangeToOverworld();
        SceneManager.LoadScene(key);
        StartCoroutine(LoadOWSceneDelayFromMenu());
    }
    public void LoadBattleScene()
    {
        FindObjectOfType<PlayerControlsManager>().ChangeToBattle();
        GameEvents.OnSaveBetweenScenes();
        SceneManager.LoadScene("BattleScene");
    }

    public void LoadSaveScene()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOnGenericUI();
        SceneManager.LoadScene("Savefile Menu");
    }

    public IEnumerator LoadOWSceneDelayFromMenu()
    {
        while(FindObjectOfType<PlayerController>() == null)
        {
            yield return null;
        }
        GameEvents.LoadInitiated("");
    }

    public IEnumerator LoadOWSceneDelay()
    {
        while (FindObjectOfType<PlayerController>() == null)
        {
            yield return null;
        }
        GameEvents.OnLoadBetweenScenes();
    }

    /*
     * int currentSceneIndex;
    [SerializeField] int timeToWait = 4;


    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
     * 
     * */
}
