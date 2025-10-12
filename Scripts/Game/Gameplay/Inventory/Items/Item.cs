namespace myProject{
    public class Item : IItem{
        public ItemsType itemId{ get; private set; }

        public Item(ItemsType itemId){
            this.itemId = itemId;
        }
    }
}