
using Audio;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonWithAudio : Button
    {
        protected override void Start()
        {
            base.Start();
            onClick.AddListener(PlayAudio);
        }

        //Método que chama o método de Tocar áudio
        private void PlayAudio()
        {
            AudioManager.instance.PlayGenericButtonSound();
        }
    }
}