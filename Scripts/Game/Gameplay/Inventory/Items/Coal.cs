namespace myProject{
    public class Coal : IItem{
        public ItemsType itemId{ get; private set; }

        public Coal(){
            itemId = ItemsType.Coal;
        }
    }
}