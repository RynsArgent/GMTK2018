using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WaveManager : MonoBehaviour
{
    // Waves in order.
    private List<PlayableDirector> waves;

    public void Awake()
    {
        waves = new List<PlayableDirector>(GetComponentsInChildren<PlayableDirector>());
    }

    public void Start()
    {
        waves[0].Play();
    }
}
