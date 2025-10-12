using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace myProject{
    public class SceneLoadManager{
        private readonly Coroutines _coroutines;
        private readonly UiRootView _uiRootView;
        private readonly DiContainer _rootContainer;
        private DiContainer _cachedSceneContainer;

        public SceneLoadManager(DiContainer rootContainer,Coroutines coroutines, UiRootView uiRootView){
            _rootContainer = rootContainer;
            _coroutines = coroutines;
            _uiRootView = uiRootView;
        }
        
        public void RunGame(){
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == Scenes.GAMEPLAY){
                _coroutines.StartCoroutine(LoadAndStartGameplay());
                return;
            }

            if (sceneName == Scenes.MAIN_MENU){
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if (sceneName != Scenes.BOOT){
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartGameplay(){
            _uiRootView.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(0.01f);
            
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState();
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DiContainer(_rootContainer); // создаем новы контейнер и передаем в гемплей
            sceneEntryPoint.Run(gameplayContainer);

            sceneEntryPoint.GoToMainMenuSceneRequested += () => {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            };


            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(){
            _uiRootView.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(0.01f);

            var mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEnterPoint>();
            var mainMenuContainer = _cachedSceneContainer = new DiContainer(_rootContainer);
            mainMenuEntryPoint.Run(mainMenuContainer);

            mainMenuEntryPoint.GoToGameplaySceneRequested += () => {
                _coroutines.StartCoroutine(LoadAndStartGameplay());
            };
            
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName){
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        public void LoadMainMenuScene(){
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }
        
        public void LoadGameplayScene(){
            _coroutines.StartCoroutine(LoadAndStartGameplay());
        }
    }
}