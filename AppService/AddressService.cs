using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class AddressService : IAddress
    {
        private AppDbContext _dbContext;
        public AddressService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
        }

        public void AddAddressToOrder(int AddressId, int OrderId)
        {
            var order = _dbContext.Orders
                //.Include(a => a.Address)
                .FirstOrDefault(o => o.Id == OrderId);
            if(order != null)
            {
                var address = GetAddress(AddressId);
                if (address != null)
                {
                    order.Address = address;
                    _dbContext.SaveChanges();
                }
            }
        }

        public void DeleteAddress()
        {
            throw new NotImplementedException();
        }

        public Address GetAddress(int AddressId)
        {
            return GetAll().FirstOrDefault(a => a.Id == AddressId);
        }

        public IEnumerable<Address> GetAll()
        {
            return _dbContext.Addresses;
        }
    }
}
