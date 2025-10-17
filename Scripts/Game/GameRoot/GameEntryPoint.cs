using System.Collections;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Common;
using myProject.Scripts.Game.Gameplay.Root;
using myProject.Scripts.Game.GameRoot.Services;
using myProject.Scripts.Game.MainMenu.Root;
using myProject.Scripts.Game.Settings;
using myProject.Scripts.Game.State.Providers;
using myProject.Scripts.Utility;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace myProject.Scripts.Game.GameRoot{
    public class GameEntryPoint{
        private static GameEntryPoint _instance;
        private readonly Coroutines _coroutines;
        private  UiRootView _uiRootView;
        private readonly DiContainer _rootContainer = new();
        private DiContainer _cachedSceneContainer;
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //запускаеться автоматически до загрузки сцены
        public static void AutoStartGame(){
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint(){
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UiRootView>("UI/UiRoot");
            _uiRootView = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);
            _rootContainer.RegisterInstance(_uiRootView);

            var settingsProvider = new SettingsProvider();
            _rootContainer.RegisterInstance<ISettingsProvider>(settingsProvider);

            var gameStateProvider = new PlayerPrefsGameStateProvider();
            gameStateProvider.LoadSettingsState();
            _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);
            
            _rootContainer.RegisterFactory(c => new SomeCommonService()).AsSingle();
        }

        private async void RunGame(){
            await _rootContainer.Resolve<ISettingsProvider>().LoadGameSettings();
            
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == Scenes.GAMEPLAY){
                var enterParams = new GameplayEnterParams("ddd.save", 1);
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));
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

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams){
            _uiRootView.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(0.01f);

            var isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);
            
            var gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DiContainer(_rootContainer);
            gameplayEntryPoint.Run(gameplayContainer, enterParams).Subscribe(gameplayExitParams => {
                _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.mainMenuEnterParams));
            });
            
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null){
            _uiRootView.ShowLoadingScreen();
            _cachedSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(0.01f);

            var mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEnterPoint>();
            var mainMenuContainer = _cachedSceneContainer = new DiContainer(_rootContainer);
            mainMenuEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams => {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;
                if (targetSceneName == Scenes.GAMEPLAY){
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });
            
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName){
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}