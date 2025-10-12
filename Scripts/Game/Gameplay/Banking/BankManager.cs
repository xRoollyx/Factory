using System;
using UnityEngine;

namespace myProject{
    public class BankManager : MonoBehaviour{
        
        public void Initialize(MoneyData moneyData){
            GoldInteractor goldInteractor = new GoldInteractor(moneyData);
            Bank.InitializeBank(goldInteractor);
        }
    }
}