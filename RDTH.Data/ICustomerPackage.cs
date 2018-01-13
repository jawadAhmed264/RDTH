using RDTH.Data.Models;
using System;
using System.Collections.Generic;

namespace RDTH.Data
{
    public interface ICustomerPackage
    {
        IEnumerable<CustomerPackage> GetAll();
        CustomerPackage GetById(int customerId);
        DateTime GetExpirationTime(int customerId);
        Status GetRechargeStatus(int customerId);
        Package GetPackage(int customerId);
    }
}
