using System;
using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    [Serializable]
    public class InventoryData{
        public Vector2Int inventorySize;
        public SlotData[] slots;
    }
}