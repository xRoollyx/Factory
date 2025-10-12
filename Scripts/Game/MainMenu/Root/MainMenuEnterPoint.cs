using UnityEngine;

namespace myProject{
    public class MainMenuEnterPoint : MonoBehaviour{
        private  DiContainer _mainMenuContainer;
        public void Run(DiContainer container){
            _mainMenuContainer = container;
        }
        
        private void Update(){
            if (Input.GetKeyDown(KeyCode.Space)){
                var sceneLoadManager = _mainMenuContainer.Resolve<SceneLoadManager>();
                sceneLoadManager.LoadGameplayScene();
            }
        }
    }
}