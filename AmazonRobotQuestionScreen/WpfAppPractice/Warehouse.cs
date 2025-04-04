using System.IO.Packaging;
using System.Numerics;

namespace WpfAppPractice
{
    internal class Warehouse
    {
        public List<StorageUnit> StorageUnits {   get; private set; }
        public readonly SortedDictionary<Size, List<StorageUnit>> AvailableStorage;
        private SortedSet<Package> PendingPackages;

        public Warehouse()
        {
            AvailableStorage = new SortedDictionary<Size, List<StorageUnit>>
        {
            { Size.Small, new List<StorageUnit>() },
            { Size.Medium, new List<StorageUnit>() },
            { Size.Large, new List<StorageUnit>() }
        };
            PendingPackages = new SortedSet<Package>(Comparer<Package>.Create((a, b) => a.PackageSize.CompareTo(b.PackageSize)));

            foreach (var unit in new List<StorageUnit>
        {
            new StorageUnit(Size.Small, new Vector2(10, 10)), new StorageUnit(Size.Small, new Vector2(20, 50)),
            new StorageUnit(Size.Medium, new Vector2(50, 30)), new StorageUnit(Size.Medium, new Vector2(180, 10)),
            new StorageUnit(Size.Large, new Vector2(260, 10)), new StorageUnit(Size.Large, new Vector2(360, 10))
        })
            {
                AvailableStorage[unit.UnitSize].Add(unit);
            }
        }

        public void AddPackages(IEnumerable<Package> packages)
        {
            foreach (var package in packages)
            {
                PendingPackages.Add(package);
            }
            TryStorePackages();
        }

        private void TryStorePackages()
        {
            foreach (var package in PendingPackages.ToList())
            {
                if (StorePackage(package))
                {
                    PendingPackages.Remove(package);
                }
            }
        }

        public bool StorePackage(Package package)
        {
            foreach (var size in Enum.GetValues(typeof(Size)).Cast<Size>())
            {
                if (size >= package.PackageSize && AvailableStorage[size].Any(unit => !unit.IsOccupied))
                {
                    var unit = AvailableStorage[size].First(unit => !unit.IsOccupied);
                    unit.StorePackage(package);
                    return true;
                }
            }
            return false;
        }
    }
}