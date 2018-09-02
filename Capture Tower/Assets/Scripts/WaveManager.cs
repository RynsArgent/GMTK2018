using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class WaveManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text gameOverText;

    // Waves in order.
    private List<PlayableDirector> waves;
    int currentWaveIndex;
    
    public void Awake()
    {
        waves = new List<PlayableDirector>(GetComponentsInChildren<PlayableDirector>());
        currentWaveIndex = 0;
    }

    public void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("FINISHED ALL WAVES");
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
                if (gameOverText != null)
                {
                    gameOverText.text = "You Win!";
                }
            }
            return;
        }

        waves[currentWaveIndex].Play();
        waves[currentWaveIndex].stopped += OnWaveFinished;
    }

    public void OnWaveFinished(PlayableDirector wave)
    {
        Debug.Log("Finished Wave " + currentWaveIndex.ToString());
        currentWaveIndex++;
        StartWave();
    }
}
