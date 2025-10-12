using UnityEngine;

namespace myProject{
    public static class Bank{
        private static GoldInteractor _goldInteractor;

        public static void InitializeBank(GoldInteractor goldInteractor){
            _goldInteractor = goldInteractor;
            _goldInteractor.onGoldChanged += PrintGold;
        }

        public static void AddGold(object sender, int amount){
            _goldInteractor.Add(sender, amount);
        }

        public static void PrintGold(int amount){
            Debug.Log("Gold: " + amount);
        }
    }
}