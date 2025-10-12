using System;
using myProject.Scripts.Game.GameRoot.Services;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Services{
    public class SomeGameplayService: IDisposable{
        
        private readonly SomeCommonService  _someCommonService;

        public SomeGameplayService(SomeCommonService someCommonService){
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + "has been created");
        }
        public void Dispose(){
            Debug.Log("SomeGameplayService disposed");
        }
    }
}