using System;
using System.Collections.Generic;
using System.Linq;
using Task_ASP.BL.DTO;
using Task_ASP.BL.Mapping;
using Task_ASP.DAL;
using Task_ASP.DAL.Entities;
using Task_ASP.Library;

namespace Task_ASP.BL
{
    public class ClientManager : IClientManager
    {
        private readonly DBContext _context;

        public ClientManager()
        {
            _context = new DBContext();
        }
        private bool ValidateClient(Client client, out string errorStr)
        {
            if (client.Name == "" || client.Name == null)
            {
                errorStr = "Client Name is mandatory";
                return false;
            }

            int clientNum = (from cl in _context.Clients
                             where (cl.Name == client.Name)
                                && (cl.ID != client.ID)
                             select cl).Count();
            if (clientNum > 0)
            {
                errorStr = "Client Name should be unique";
                return false;
            }

            errorStr = "";
            return true;
        }
        public IOperationResult AddClient(DTO_Client dtoClient)
        {
            Client client = dtoClient.ToClient();
            client.ID = 0;

            string errorStr;
            if (!ValidateClient(client, out errorStr))
            {
                return new OperationWithErrors(errorStr);
            }

            _context.Clients.Add(client);
            _context.SaveChanges();
            return new SuccessfullOperation("Client added successfully");
        }
        public IOperationResult RemoveClient(int clientID)
        {
            Client clientToRemove = (from cl in _context.Clients
                                     where cl.ID == clientID
                                     select cl).First();
            if (clientToRemove == null)
                return new OperationWithErrors(string.Format("Cant find such client: {0}", clientID));

            clientToRemove.IsDeleted = true;
            _context.SaveChanges();
            return new SuccessfullOperation("Client removed");
        }
        public IOperationResult ModifyClient(DTO_Client dtoClient)
        {
            Client client = (from cl in _context.Clients
                             where cl.ID == dtoClient.ID
                             select cl).First();
            if (client == null)
            {
                return new OperationWithErrors(string.Format("Ivalid client, Name: {0}", dtoClient.Name));
            }

            Client newClient = dtoClient.ToClient();
            string errorStr;
            if (!ValidateClient(newClient, out errorStr))
            {
                return new OperationWithErrors(errorStr);
            }

            client.Name = newClient.Name;
            _context.SaveChanges();
            return new SuccessfullOperation("Client modified successfully");
        }
        public List<DTO_Client> Clients()
        {
            return (from cl in _context.Clients
                    where !cl.IsDeleted
                    select cl
                   ).ToList().ToDTOClientsList();
        }
        public DTO_Client ClientByID(int clientID)
        {
            return (from cl in _context.Clients
                    where cl.ID == clientID
                    select cl).First().ToManagerDTO_Client();
        }
        public List<DTO_Order> ClientsOrders(int clientID)
        {
            return (from o in _context.Orders
                    where (o.ClientID == clientID)
                    select o
                   ).ToList().ToManagerOrdersList();
        }
        public List<DTO_Order> ClientsOrders(int clientID, int strartID, int ordersLimit)
        {
            return (from o in _context.Orders
                    where (o.ClientID == clientID) && (o.ID > strartID)
                    select o
                   ).Take(ordersLimit).ToList().ToManagerOrdersList();


        }
        public void ClientsOrdersPagingInfo(int clientID, out int firstID, out int lastID)
        {
            var request = (from o in _context.Orders
                           where (o.ClientID == clientID)
                           select o.ID
                          );
            if (request != null)
            {
                firstID = request.Min();
                lastID = request.Max();
            }
            else
            {
                firstID = 0;
                lastID = 0;
                throw new Exception("Invalid request parameters");
            }
        }
    }
}