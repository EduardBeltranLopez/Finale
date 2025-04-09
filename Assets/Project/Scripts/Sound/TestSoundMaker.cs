using GamePlay;
using UnityEngine;

namespace GamePlay
{
    public class TestSoundMaker : MonoBehaviour
    {
        [SerializeField] private AudioSource source = null;
        [SerializeField] private float soundRange = 25f;
        [SerializeField] private Sound.SoundType soundType = Sound.SoundType.Dangerous;


        private void OnMouseDown()
        {
            if (source.isPlaying) 
                return;

            source.Play();

            var sound = new Sound(transform.position, soundRange, soundType);

            Sounds.MakeSound(sound);
        }
    }
}