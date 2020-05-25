using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IAddress
    {
        void AddAddress(Address address);
        void DeleteAddress();
        IEnumerable<Address> GetAll();
        Address GetAddress(int AddressId);
        void AddAddressToOrder(int AddressId, int OrderId);
    }
}
