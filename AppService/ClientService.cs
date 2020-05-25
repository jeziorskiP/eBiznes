using DataContext;
using DataContext.Interfaces;
using DataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppService
{
    public class ClientService : IClient
    {
        private AppDbContext _dbContext;
        public ClientService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddClient(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public void AddClientToOrder(int ClientId, int OrderId)
        {
            var order = _dbContext.Orders
                //.Include(a => a.Address)
                .FirstOrDefault(o => o.Id == OrderId);
            if (order != null)
            {
                var client = GetClient(ClientId);
                if (client != null)
                {
                    order.Client = client;
                    _dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Client> GetAll()
        {
            return _dbContext.Clients;
        }

        public Client GetClient(int ClientId)
        {
            return GetAll().FirstOrDefault(c => c.Id == ClientId);
        }

        public int NextNumber()
        {
            if (GetAll().Count() == 0)
                return 10000;
            else
                return GetAll().OrderBy(c => c.ClientNumber).Last().ClientNumber + 1;
        }
    }
}
