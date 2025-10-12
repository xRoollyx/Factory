using R3;
using UnityEditor;
using UnityEngine;

namespace myProject{
    public class Slot{
        private SlotData _slotData;
        private int maxStack = 64;
        private int _index;
        public ReactiveProperty<ItemsType> itemId { get; private set; }
        public ReactiveProperty<int> amountItem { get; private set; }
        

        
        
        public bool isEmpty => itemId.Value == ItemsType.Null;

        public Slot(SlotData slotData, int index){
            _index = index;
            _slotData = slotData;
            itemId = new ReactiveProperty<ItemsType>(slotData.itemId);
            amountItem = new ReactiveProperty<int>(slotData.amount);
            itemId.Subscribe(_ => PrintInventory());
            amountItem.Subscribe(_ => PrintInventory());
        }

        public int AddItem(IItem item, int amount){
            if (itemId.Value == item.itemId && amount > 0){
                if (amountItem.Value + amount <= maxStack){
                    amountItem.Value += amount;
                    _slotData.amount = amountItem.Value;
                    return 0;
                }

                var res = amountItem.Value + amount - maxStack;
                amountItem.Value = maxStack;
                _slotData.amount = amountItem.Value;
                return res;
            }

            if (itemId.Value == ItemsType.Null && amount > 0){
                itemId.Value = item.itemId;
                _slotData.itemId = itemId.Value;
                if (amountItem.Value + amount <= maxStack){
                    amountItem.Value += amount;
                    _slotData.amount = amountItem.Value;
                    return 0;
                }

                var res = amountItem.Value + amount - maxStack;
                amountItem.Value = maxStack;
                _slotData.amount = amountItem.Value;
                return res;
            }

            return -1; // error
        }

        public int RemoveItem(IItem item, int amount){
            if (itemId.Value == item.itemId && amount > 0){
                if (amountItem.Value - amount > 0){
                    amountItem.Value -= amount;
                    _slotData.amount = amountItem.Value;
                    return 0;
                }

                if (amountItem.Value - amount == 0){
                    itemId.Value = ItemsType.Null;
                    _slotData.itemId = ItemsType.Null;
                    amountItem.Value = 0;
                    _slotData.amount = amountItem.Value;
                    return 0;
                }

                if (amountItem.Value - amount < 0){
                    var res = amount - amountItem.Value;
                    itemId.Value = ItemsType.Null;
                    _slotData.itemId = ItemsType.Null;
                    amountItem.Value = 0;
                    _slotData.amount = amountItem.Value;
                    return res;
                }
            }

            return -1; // error
        }

        public void PrintInventory(){
            Debug.Log($"{_index} предмет: {itemId.Value} в колличестве {amountItem.Value}" );
        }
    }
}