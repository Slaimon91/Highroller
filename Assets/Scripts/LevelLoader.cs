using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //int currentSceneIndex;
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
    // Start is called before the first frame update
    void Start()
    {
        //currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadOverworldScene()
    {
        if(SceneManager.GetActiveScene().name == "BattleScene")
        {
            StartCoroutine(GiveBattleBounty());
        }
        FindObjectOfType<PlayerControlsManager>().ChangeToOverworld();
        SceneManager.LoadScene("OverworldScene");
    }
    public void LoadBattleScene()
    {
        FindObjectOfType<PlayerControlsManager>().ChangeToBattle();
        SceneManager.LoadScene("BattleScene");
    }

    IEnumerator GiveBattleBounty()
    {
        while (SceneManager.GetActiveScene().name != "OverworldScene")
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().name == "OverworldScene")
        {
            FindObjectOfType<BattleBounty>().GiveBounty();
        }
        yield return null;
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
