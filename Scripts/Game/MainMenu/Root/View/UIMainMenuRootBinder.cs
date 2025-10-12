using System;
using R3;
using UnityEngine;

namespace myProject.Scripts.Game.MainMenu.Root.View{
    public class UIMainMenuRootBinder: MonoBehaviour{

        private Subject<Unit> _exitSceneSignalSubj;
        public void HandleGameplayButtonClicked(){
            _exitSceneSignalSubj.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubj){
            _exitSceneSignalSubj = exitSceneSignalSubj;
        }
    }
}