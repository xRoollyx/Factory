using UnityEngine;

namespace myProject{
    public class GameEntryPoint{
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UiRootView _uiRootView;
        private readonly DiContainer _rootContainer = new();
        

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
            
            var sceneLoadManager = new SceneLoadManager(_rootContainer, _coroutines, _uiRootView);
            _rootContainer.RegisterInstance(sceneLoadManager);

            var gameStateProvider = new PlayerPrefsGameStateProvider();
            _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);
        }

        private void RunGame(){
            _rootContainer.Resolve<SceneLoadManager>().RunGame();
        }
    }
}