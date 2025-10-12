using R3;
using UnityEngine;

namespace myProject{
        public class Refinery : BaseBuilding {
            private float timeOut = 2f;
            private BuildingSlot slot;
            
            
            public Refinery(BuildManager buildManager, BuildingsSo buildingsSo, int buildId) : base(buildManager, buildingsSo, buildId){
                slot = new BuildingSlot();
               
            }

            public override void Update(float deltaTime){
                timeOut -= deltaTime;
                if (timeOut < 0){
                    if (slot.SetItem(ItemsType.Coal, 1, 5) != 0){
                        slot.GetItemAmount(ItemsType.Coal, 5);
                    };
                    timeOut = 2f;
                }
            }

            public override void Interact(){
                _buildManager.OpenBuildingMenu(slot.GetAmount(), slot.itemId, this);
                Debug.Log(buildId);
            }

            public ReactiveProperty<int> PrintAmountItem(){
                return slot.GetAmount();
            }

            public ReactiveProperty<ItemsType> PrintItemId(){
                return slot.itemId;
            }
        }
    }
