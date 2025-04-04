using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPractice
{

    internal class Package
    {
        public Size PackageSize { get; }

        public Package(Size size)
        {
            PackageSize = size;
        }
    }
}
