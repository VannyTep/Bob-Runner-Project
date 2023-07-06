using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeadUI : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Play Button Sounds
        /* Here is the example => */ FindObjectOfType<AudioManager>().Play("ButtonSound");
    }

    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        // Play Button Sounds
    }
}
