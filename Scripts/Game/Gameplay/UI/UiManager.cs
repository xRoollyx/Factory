using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace myProject{
    public class UiManager : MonoBehaviour
    {
        [FormerlySerializedAs("buildingMenu")] [SerializeField] private UiBuildingMenu uiBuildingMenu;
        
        private readonly List<IDisposable> _disposables = new ();
        private BaseBuilding _currentBuilding;

        private UnityAction _onDestroy;

        public void ShowBuildMenu(ReactiveProperty<int> amount, ReactiveProperty<ItemsType> itemType, BaseBuilding building){
            HideBuildMenu();

            _currentBuilding = building;
            _onDestroy += DestroyBuilding;
            _disposables.Add(itemType.Subscribe(uiBuildingMenu.PrintItemId));
            _disposables.Add(amount.Subscribe(uiBuildingMenu.PrintAmountItem));
            uiBuildingMenu.SubscribeDestroyButton(_onDestroy);
            uiBuildingMenu.ShowPanel();
        }

        public void HideBuildMenu(){
            uiBuildingMenu.HidePanel();
            foreach (var disposable in _disposables){
                disposable.Dispose();
            }
        }

        private void DestroyBuilding(){
            _currentBuilding.Destroy();
            _onDestroy -= DestroyBuilding;
            uiBuildingMenu.HidePanel();
        }
    }
}
