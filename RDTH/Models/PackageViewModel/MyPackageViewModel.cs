using RDTH.Models.SetBoxViewModel;
using System;

namespace RDTH.Models.PackageViewModel
{
    public class MyPackageViewModel
    {
        public PackageDetailViewModel MyPackage { get; set; }
        public SetBoxDetailModel MySetBox { get; set; }
        public DateTime GetExpiration { get; set; }
        public string State { get; set; }
    }
}
