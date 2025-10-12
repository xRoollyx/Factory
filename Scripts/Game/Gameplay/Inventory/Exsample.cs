using System;
using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    public class Exsample: MonoBehaviour{
        public Inventory inventory;
        public ItemsType itemType;
        public Coal coal;
        private void Start(){
            coal = new Coal();
            var data = new InventoryData{
                inventorySize = new Vector2Int(2, 2),
                slots = new SlotData[4]
            };
            inventory = new Inventory(data);
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.Space)){
                inventory.SetItemInSlot(coal, 10);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)){
                inventory.SetItemInSlot(coal, 15, new Vector2Int(1,1));
            }
            
        }
    }
}