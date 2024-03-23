using Managers;
using UnityEngine.UI;

namespace UI
{
    public class ExitButton : ButtonWithAudio
    {
        protected override void Start()
        {
            base.Start();
            onClick.AddListener(ExitGame);
        }

        //Método que chama o método de sair do jogo do GameManager
        private void ExitGame()
        {
            GameManager.instance.ExitGame();
        }
    }
}