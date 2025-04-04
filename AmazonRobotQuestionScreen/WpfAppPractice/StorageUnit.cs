using System.Numerics;
using System.Windows.Xps.Packaging;

namespace WpfAppPractice
{
    public enum Size { Small, Medium, Large }

    internal class StorageUnit
    {
        public Size UnitSize { get; }
        public bool IsOccupied { get; private set; }
        public Vector2 Position { get; private set; }
        public Package? StoredPackage { get; private set; }

        public StorageUnit(Size size, Vector2 pos)
        {
            UnitSize = size;
            IsOccupied = false;
            Position = pos;
            StoredPackage = null;
        }
        private bool CanFit(Size size) {
            if (this.UnitSize == Size.Large)
            {
                return true;
            }
            else if (this.UnitSize == Size.Medium)
            {
                if (size.Equals(Size.Large))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { return true; }
        }
        public bool StorePackage(Package package)
        {
            if (!IsOccupied && CanFit(package.PackageSize))
            {
                this.StoredPackage = package;
                IsOccupied = true;
                return true;
            }
            return false;
        }
    }
}