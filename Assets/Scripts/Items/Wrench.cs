namespace Items
{
    public class Wrench: Item
    {
        private float _durability;
        
        public Wrench() : base(ItemType.Wrench)
        {
            _durability = 1f;
        }
    }
}