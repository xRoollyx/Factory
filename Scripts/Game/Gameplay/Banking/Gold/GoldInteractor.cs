using System;

namespace myProject{
    public class GoldInteractor{
        public Action<int> onGoldChanged;
        private readonly MoneyData _moneyData;
        public int gold => _moneyData.gold;

        public GoldInteractor(MoneyData moneyData){
            _moneyData =  moneyData;
            onGoldChanged?.Invoke(moneyData.gold);
            
        }

        public void Add(object sender, int amount){
            _moneyData.gold += amount;
            onGoldChanged?.Invoke(gold);
        }

        public void Spend(object sender, int amount){
            if (_moneyData.gold >= amount){
                _moneyData.gold -= amount;
                onGoldChanged?.Invoke(gold);
            }
        }

        public bool CanSpend(object sender, int amount){
            return _moneyData.gold >= amount;
        }
    }
}