using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface IPackageService
    {
        IEnumerable<Package> GetAll();
        IEnumerable<Package> GetLatest();
        Package GetById(int id);
        void AddPackage(Package newPackage);

    }
}
