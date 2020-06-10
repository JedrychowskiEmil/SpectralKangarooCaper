using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void startGame()
    {
        SoundController.playSound("button");
        SceneManager.LoadScene(1);
    }

    public void goToMenu()
    {
        SoundController.playSound("button");
        SceneManager.LoadScene(0);
    }
}
