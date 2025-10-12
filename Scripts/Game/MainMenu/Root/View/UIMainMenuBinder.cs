using System;
using UnityEngine;

namespace myProject.Scripts.Game.MainMenu.Root.View{
    public class UIMainMenuBinder : MonoBehaviour{
        public event Action GoToGameplayButtonClicked;

        public void HandleGameplayButtonClicked(){
            GoToGameplayButtonClicked?.Invoke();
        }
    }
}