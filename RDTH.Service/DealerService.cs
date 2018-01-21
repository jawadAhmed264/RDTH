using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTH.Service
{
    public class DealerService : IDealerService
    {
        private RDTHDbContext _con;

        public DealerService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(Dealer newDealer)
        {
            _con.Dealers.Add(newDealer);
        }

        public IEnumerable<Dealer> GetAll()
        {
            return _con.Dealers.Include(m => m.Orders).
                Include(m => m.Payments).Include(m => m.ApplicationUser);
        }

        public Dealer GetById(int DealerId)
        {
            return GetAll().FirstOrDefault(d => d.Id == DealerId);
        }

        public Dealer GetByUserId(string UserId)
        {
            return GetAll().SingleOrDefault(m => m.ApplicationUser.Id == UserId);
        }

        public IEnumerable<Order> GetOrders(int DealerId)
        {
            return _con.Orders.Include(o=>o.Details).
                Include(o=>o.Status).
                Where(o=>o.Dealer.Id==DealerId);
        }

        public IEnumerable<Payment> GetPayments(int DealerId)
        {
            return GetById(DealerId).Payments;
        }

        public async Task<int> PlacedOrderAsync(Dealer dealer, Cart cart, Order newOrder, Payment newPayment)
        {
            newOrder.Dealer = dealer;
            newOrder.Status =await _con.Status.SingleOrDefaultAsync(s=>s.Name== "Pending");
            newOrder.TotalItems = cart.TotalItems;
            newOrder.TotalPrice = cart.TotalPrice;
            await _con.Orders.AddAsync(newOrder);

            foreach (var item in cart.ItemList) {
                OrderDetail orderDetail = new OrderDetail {
                    Order = newOrder,
                    Product =await _con.SetBoxes.SingleOrDefaultAsync(s=>s.Id==item.Product.Id),
                    Price=item.Price,
                    Qty=item.Qty
                };
                await _con.OrderDetails.AddAsync(orderDetail);
            }

            newPayment.Dealer = dealer;
            newPayment.Order = newOrder;
            await _con.Payments.AddAsync(newPayment);
            int result =await _con.SaveChangesAsync();
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
