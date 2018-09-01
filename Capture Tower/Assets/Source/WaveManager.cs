using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WaveManager : MonoBehaviour
{
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
