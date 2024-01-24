using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADWatering;
    public AudioClip ADFishingStart;
    public AudioClip ADFishing;
    public AudioClip ADBuySuccess;
    public AudioClip ADHarvest;
    public AudioClip ADGetBread;
    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD" + action);
        audioSource = go.AddComponent<AudioSource>();
        DontDestroyOnLoad(go);

        switch (action)
        {
            case "Watering":
                audioSource.clip = ADWatering; break;
            case "FishingStart":
                audioSource.clip = ADFishingStart; break;
            case "Fishing":
                audioSource.clip = ADFishing; break;
            case "BuySuccess":
                audioSource.clip = ADBuySuccess; break;
            case "Harvest":
                audioSource.clip = ADHarvest; break;
            case "GetBread":
                audioSource.clip = ADGetBread; break;
        }
        audioSource.Play();
        Destroy(go, audioSource.clip.length);
    }
}
