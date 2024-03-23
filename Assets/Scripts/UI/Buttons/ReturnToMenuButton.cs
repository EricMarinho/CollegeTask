using Managers;
using UnityEngine.UI;

namespace UI
{
    public class ReturnToMenuButton : ButtonWithAudio
    {
        protected override void Start()
        {
            base.Start();
            onClick.AddListener(ReturnToMenu);
        }

        //Método que chama o método de sair do jogo do GameManager
        private void ReturnToMenu()
        {
            GameManager.instance.GoToMenu();
        }
    }
}
