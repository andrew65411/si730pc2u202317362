using System;

namespace Sales.Domain
{
    public class Fabric
    {
        public EFabric Value { get; }
        public int Id => (int)Value;
        public string Name => Value.ToString();

        public Fabric(int id)
        {
            if (!Enum.IsDefined(typeof(EFabric), id))
                throw new ArgumentException("Invalid fabric id");
            Value = (EFabric)id;
        }

        public Fabric(EFabric value)
        {
            if (!Enum.IsDefined(typeof(EFabric), value))
                throw new ArgumentException("Invalid fabric value");
            Value = value;
        }
    }
}
