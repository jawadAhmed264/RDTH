using RDTH.Data.Models;
using System.Collections.Generic;

namespace RDTH.Areas.Admin.Models.AdminViewModel
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Payment> Payments { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Recharge> RechargeCatalog { get; set; }
        public IEnumerable<FeedBack> Feedbacks { get; set; }

        public int LatestRequest { get; set; }
        public int LatestSubscribe { get; set; }
        public int LatestOrders { get; set; }
        public int LatestMOD { get; set; }

    }
}
