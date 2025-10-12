using UnityEngine;
using UnityEngine.Events;

namespace myProject{
    public class Coins{
        public UnityEvent<int> changeCoinsValue;
        private int _value;

        public int value{
            get{ return _value; }
            private set{ _value = value; }
        }

        public void AddCoins(int count){
            if (count < 0){
                Debug.LogError("negative count");
            }
            value += count;
            changeCoinsValue?.Invoke(value);
        }

        public bool HasCoins(int count){
            if (value - count >= 0){
                return true;
            }

            return false;
        }

        public bool SpendCoins(int count){
            if (value - count < 0){
                return false;
            }

            value -= count;
            changeCoinsValue?.Invoke(value);
            return true;
        }
    }
}