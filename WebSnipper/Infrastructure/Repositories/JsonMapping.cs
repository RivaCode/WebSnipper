namespace Infrastructure.Repositories
{
    public sealed class StoreKey
    {
        private readonly string _name;
        private StoreKey(string name) => _name = name;

        public static StoreKey Sites { get; } = new StoreKey("sites");
        public static StoreKey Settings { get; } = new StoreKey("settings");

        public override bool Equals(object obj) 
            => (obj as StoreKey)?._name.Equals(_name) ?? false;

        public override int GetHashCode() => _name.GetHashCode();

        public override string ToString() => _name;
    }
}