using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Audio
{
    public class MediaPlayer: MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> clips;

        [SerializeField] private float delayTime;
        private float _elapsedTime;
        
        private AudioSource _audioSource;

        private Random _random;
        private int clipIndex;

        private void Awake()
        {
            clipIndex = clips.Count;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _random = new Random();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= delayTime)
                {
                    _elapsedTime = 0f;
                    _audioSource.clip = NextClip();
                    _audioSource.Play();
                }
            }
        }

        private AudioClip NextClip()
        {
            if (clipIndex >= clips.Count)
            {
                clipIndex %= clips.Count;
                RandomShuffle(clips);
            }

            return clips[clipIndex++];
        }

        private void RandomShuffle<T>(IList<T> list)
        {
            for (var i = list.Count - 1; i >= 1; i--)
            {
                var j = _random.Next(i);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}