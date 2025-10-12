using R3;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace myProject{
    public class BuildingSlot{

        public ReactiveProperty<ItemsType> itemId{ get; private set; } = new();
        private ReactiveProperty<int> _amount = new ();
        private int _maxStack;

        public bool isEmpty => itemId.Value == ItemsType.Null;
        public bool isFull => _maxStack == _amount.Value;
        
        

        public int SetItem(ItemsType item, int amount, int maxStack = 1){
            if (itemId.Value == ItemsType.Null){
                itemId.Value = item;
                _maxStack = maxStack;
                if (maxStack >= amount){
                    _amount.Value = amount;
                    return 0;
                }
                else{
                    _amount.Value = maxStack;
                    return amount - maxStack;
                }
            }
            else if(itemId.Value == item){
                if (_amount.Value + amount <= maxStack){
                    _amount.Value += amount;
                    return 0;
                }
                else{
                    var result = _amount.Value + amount - maxStack;
                    _amount.Value = maxStack;
                    return result;
                }
            }
            else{
                return amount;
            }
        }

        public ReactiveProperty<int> GetAmount(){
            return _amount;
        }

        public int GetItemAmount(ItemsType item, int amount){
            if (itemId.Value == item){
                if (_amount.Value >= amount){
                    _amount.Value -= amount;
                    if (_amount.Value == 0){
                        itemId.Value = ItemsType.Null;
                    }
                    return 0;
                }
                if (_amount.Value < amount){
                    var result = amount - _amount.Value;
                    itemId.Value = ItemsType.Null;
                    _amount.Value = 0;
                    _maxStack = 1;
                    
                    return result;
                }
            }

            return amount;
        }
    }
}