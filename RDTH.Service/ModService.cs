using Microsoft.EntityFrameworkCore;
using RDTH.Data;
using RDTH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTH.Service
{
    public class ModService : IMod
    {
        private readonly RDTHDbContext _con;

        public ModService(RDTHDbContext con)
        {
            _con = con;
        }

        public void Add(MovieOnDemand newMovie)
        {
            _con.MoviesOnDemand.Add(newMovie);
            _con.SaveChanges();
        }

        public IEnumerable<MovieOnDemand> GetAll()
        {
            return _con.MoviesOnDemand.
                Include(m=>m.Status).
                Include(m=>m.Customer);
        }

        public IEnumerable<MovieOnDemand> GetByCustomer(int CustomerID)
        {
            return GetAll().Where(m => m.Customer.Id == CustomerID);
        }

        public MovieOnDemand GetById(int id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}
