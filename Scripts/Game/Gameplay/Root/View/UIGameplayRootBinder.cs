using System;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        public event Action GoToMainMenuButtonClicked;

        public void HandleMainMenuButtonClicked(){
            GoToMainMenuButtonClicked?.Invoke();
        }
    }
}
