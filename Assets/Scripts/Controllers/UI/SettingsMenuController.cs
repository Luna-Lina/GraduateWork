using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace StarVelocity
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;

        [SerializeField] private AudioMixerSnapshot Pause;

        private void Start()
        {
            _musicToggle.onValueChanged.AddListener(ToggleMusic);
            _soundToggle.onValueChanged.AddListener(ToggleSound);
        }

        private void FixedUpdate()
        {
            Time.timeScale = 0;
            Pause.TransitionTo(0.5f);
        }

        public void ToggleMusic(bool enabled)
        {
            float volume = enabled ? 0 : -80;
            _mixer.audioMixer.SetFloat("MusicVolume", volume);
        }

        public void ToggleSound(bool enabled)
        {
            float volume = enabled ? 0 : -80;
            _mixer.audioMixer.SetFloat("MasterVolume", volume);
        }
    }
}