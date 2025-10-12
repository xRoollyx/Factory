using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    public class Inventory{
        private Vector2Int _inventorySize;
        public List<Slot> slots = new ();

        public Inventory(InventoryData inventoryData){
            _inventorySize = inventoryData.inventorySize;
            for (int y = 0; y < _inventorySize.y; y++){
                for (int x = 0; x < _inventorySize.x; x++){
                    int index = x + y * _inventorySize.x;
                    if (inventoryData.slots[index] != null){
                        slots.Add(new Slot(inventoryData.slots[index], index));
                    }
                    else{
                        slots.Add(new Slot(new SlotData{
                            itemId = ItemsType.Null,
                            amount = 0
                        }, index));
                    }
                }
            }
        }

        public int SetItemInSlot(IItem item, int amount){
            foreach (var slot in slots){
                if (slot.itemId.Value == item.itemId && amount > 0){
                    var res = slot.AddItem(item, amount);
                    if (res == 0){
                        return 0;
                    }
                    if (res > 0){
                        amount = res;
                    }
                }
            }

            if (amount > 0){
                foreach (var slot in slots){
                    if (slot.isEmpty && amount > 0){
                        var res = slot.AddItem(item, amount);
                        if (res == 0){
                            return 0;
                        }
                        if (res > 0){
                            amount = res;
                        }
                    }
                }
            }

            return amount;
        }
        
        public int SetItemInSlot(IItem item, int amount, Vector2Int position){
            var slot = slots[position.x + position.y * _inventorySize.x];
            var res = slot.AddItem(item, amount);
            return res;
        }

        public Slot GetSlot(Vector2Int position){
            int index = position.x + position.y * _inventorySize.x;
            return slots[index];
        }
    }
}