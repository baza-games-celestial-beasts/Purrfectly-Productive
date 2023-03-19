using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSound : MonoBehaviour
{
    public static MSound inst;

    public MSoundInstance soundPrefab;

    private List<AudioClip> sounds;

    private void Awake() {
        if(inst != null) {
            Destroy(gameObject);
            return;
        }

        inst = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init() {
        sounds = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds"));
    }

    public static void Play(string soundName, Vector2 pos, float volume, float pitch = 1.0f, float randomVolume = 0.1f, float randomPitch = 0.1f) {
        if (inst == null) {
            Debug.LogError("No MSound initialized");
            return;
        }

        AudioClip clip = inst.sounds.Find(x => x.name == soundName);

        if (clip == null) {
            Debug.LogError("No sound found: " + soundName);
            return;
        }

        MSoundInstance soundInstance = Instantiate(inst.soundPrefab, pos, Quaternion.identity);
        soundInstance.audioSource.clip = clip;
        soundInstance.audioSource.volume = volume + Random.Range(-randomVolume, randomVolume);

        float finalPitch = pitch + Random.Range(-randomPitch, randomPitch);
        soundInstance.audioSource.volume = volume + Random.Range(-randomVolume, randomVolume);
        soundInstance.audioSource.pitch = finalPitch;

        float duration = clip.length / finalPitch;       

        soundInstance.audioSource.PlayOneShot(clip);
        Destroy(soundInstance.gameObject, duration);
    }
}
