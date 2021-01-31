using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerMovement player;
    public Text gameOverText;

    private bool reloading = false;
    private static GameController _instance;

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null && !_instance.reloading)
        {
            _instance.reloading = true;
            StartCoroutine(reloadScene());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public IEnumerator reloadScene()
    {
        _instance.gameOverText.enabled = true;

        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Invoke("ResetProps", 0.25f);
    }

    void ResetProps()
    {
        _instance.gameOverText.enabled = false;
        _instance.reloading = false;
        var gameScore = FindObjectOfType<GameScore>();
        gameScore.AddScore(-gameScore.score);
        var player = FindObjectOfType<PlayerSingleton>().gameObject;
        player.GetComponent<HealthController>().TakeDamage(0);
    }

    void Quit()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
