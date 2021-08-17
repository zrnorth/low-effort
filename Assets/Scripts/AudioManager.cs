using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveTrack
{
    Bad,
    Good
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private GameObject badAudioChild;

    [SerializeField]
    private GameObject goodAudioChild;

    private AudioSource badAudioSource;
    private AudioSource goodAudioSource;

    [SerializeField]
    private ActiveTrack activeTrack = ActiveTrack.Bad;

    [SerializeField]
    private bool shouldBePlaying = false;

    // Start is called before the first frame update
    void Start() {
        badAudioSource = GetAudioSourceForChild(badAudioChild);
        goodAudioSource = GetAudioSourceForChild(goodAudioChild);

        if (shouldBePlaying) {
            Play();
        } else {
            Stop();
        }
    }

    // Update is called once per frame
    void Update() {
        if (activeTrack == ActiveTrack.Bad) {
            badAudioSource.volume = 1;
            goodAudioSource.volume = 0;
        } else {
            badAudioSource.volume = 0;
            goodAudioSource.volume = 1;
        }

        if (IsSourcePlaying() == true && shouldBePlaying == false) {
            StopSource();
        } else if (IsSourcePlaying() == false && shouldBePlaying == true) {
            PlaySource();
        }
    }

    public void Play() {
        shouldBePlaying = true;
    }

    public void Stop() {
        shouldBePlaying = false;
    }

    public void SetTrack(ActiveTrack track) {
        activeTrack = track;
    }

    public void SetTime(float time) {
        badAudioSource.time = time;
        goodAudioSource.time = time;
    }

    public float GetTime() {
        return badAudioSource.time;
    }

    private bool IsSourcePlaying() {
        return badAudioSource.isPlaying || goodAudioSource.isPlaying;
    }
    private void PlaySource() {
        badAudioSource.Play();
        goodAudioSource.Play();
    }

    private void StopSource() {
        badAudioSource.Stop();
        goodAudioSource.Stop();
        badAudioSource.time = 0;
        goodAudioSource.time = 0;
    }

    AudioSource GetAudioSourceForChild(GameObject obj) {
        return obj.GetComponent<AudioSource>();
    }
}
