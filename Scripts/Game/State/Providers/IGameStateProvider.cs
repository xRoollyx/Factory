namespace myProject{
    public interface IGameStateProvider{
        public GameState gameStateOrigin{ get; }
        
        public GameState LoadGameState();
        public bool SaveGameState();
        public bool ResetGameState();
    }
}