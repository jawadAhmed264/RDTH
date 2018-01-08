using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Models.HomeViewModel
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Package> Packages { get; set; }
        public IEnumerable<SetBox> SetBoxes { get; set; }
    }
}
