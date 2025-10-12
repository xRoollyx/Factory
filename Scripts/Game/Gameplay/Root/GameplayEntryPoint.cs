using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour {

        private DiContainer _gameplayContainer;
        public void Run(DiContainer container){
            _gameplayContainer = container;
        }
    }
}