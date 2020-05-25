using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Interfaces
{
    public interface IClient
    {
        void AddClient(Client client);
        IEnumerable<Client> GetAll();
        Client GetClient(int ClientId);
        int NextNumber();
        void AddClientToOrder(int ClientId, int OrderId);

    }
}
