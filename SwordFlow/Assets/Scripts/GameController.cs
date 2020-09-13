using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerMovement player;
    public Text gameOverText;

    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && !reloading)
        {
            reloading = true;
            StartCoroutine(reloadScene());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public IEnumerator reloadScene()
    {
        gameOverText.enabled = true;

        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
