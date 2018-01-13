using RDTH.Data.Models;
using System;
using System.Collections.Generic;

namespace RDTH.Areas.Admin.Models.ModViewModel
{
    public class ModIndexModel
    {
        public IEnumerable<ModDetailModel> Mod { get; set; }
    }

    public class ModDetailModel {

        public int Id { get; set; }
        public string Movie { get; set; }
        public DateTime MovieTime { get; set; }
        public Status Status { get; set; }
        public CustomerCard Card { get; set; }
    }
}
