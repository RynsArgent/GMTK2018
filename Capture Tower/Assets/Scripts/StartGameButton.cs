using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("Jam's Scene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!!");
        Application.Quit();
    }
}
