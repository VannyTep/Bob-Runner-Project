using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeadUI : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
