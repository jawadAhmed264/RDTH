using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Service
{
    public class DistributerServicecs : IDistributerService
    {
        private readonly RDTHDbContext _con;

        public DistributerServicecs(RDTHDbContext con)
        {
            _con = con;
        }
        public IEnumerable<Distributer> GetAll()
        {
           return _con.Distributers.
                Include(d=>d.Orders).
                Include(d=>d.Payments).
                Include(d=>d.ApplicationUser);
        }

        public Distributer GetById(int Id)
        {
            return GetAll().FirstOrDefault(d=>d.Id==Id);
        }

        public Distributer GetByUserId(string UserId)
        {
            return GetAll().SingleOrDefault(m => m.ApplicationUser.Id == UserId);
        }

        public IEnumerable<Order> GetOrders(int DisId)
        {
            return _con.Orders.Include(o => o.Details).
                Include(o => o.Status).
                Where(o => o.Distributer.Id == DisId);
        }

        public IEnumerable<Payment> GetPayments(int DisId)
        {
            return GetById(DisId).Payments;
        }

        public async Task<int> PlacedOrderAsync(Distributer dis, Cart cart, Order newOrder, Payment newPayment)
        {
            newOrder.Distributer = dis;
            newOrder.Status = await _con.Status.SingleOrDefaultAsync(s => s.Name == "Pending");
            newOrder.TotalItems = cart.TotalItems;
            newOrder.TotalPrice = cart.TotalPrice;
            await _con.Orders.AddAsync(newOrder);

            foreach (var item in cart.ItemList)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    Order = newOrder,
                    Product = await _con.SetBoxes.SingleOrDefaultAsync(s => s.Id == item.Product.Id),
                    Price = item.Price,
                    Qty = item.Qty
                };
                await _con.OrderDetails.AddAsync(orderDetail);
            }

            newPayment.Distributer = dis;
            newPayment.Order = newOrder;
            await _con.Payments.AddAsync(newPayment);
            int result = await _con.SaveChangesAsync();

            if (result > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
