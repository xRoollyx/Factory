using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace myProject{
    public class UiBuildingMenu: MonoBehaviour{
        [SerializeField] private TextMeshProUGUI itemTypeId;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Button destroyButton;
        [SerializeField] private Image image;
        [SerializeField] private Sprite icon;

        private void Awake(){
            HidePanel();
        }


        public void ShowPanel(){
            gameObject.SetActive(true);
        }

        public void HidePanel(){
            gameObject.SetActive(false);
        }

        public void PrintAmountItem(int amount){
            if (amount == 0){
                itemAmount.text = "";
            }
            else{
                itemAmount.text = amount.ToString();
            }
        }
        public void PrintItemId(ItemsType itemType){
            if (itemType == ItemsType.Null){
                itemTypeId.text = "";
                image.sprite = null;
            }
            else{
                itemTypeId.text = itemType.ToString();
                image.sprite = icon;
            }
        }

        public void SubscribeDestroyButton(UnityAction onDestroy){
            
            destroyButton.onClick.AddListener(onDestroy);
        }
    }
}