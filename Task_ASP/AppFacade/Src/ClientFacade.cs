using log4net;
using System.Collections.Generic;
using System.Linq;
using Task_ASP.AppFacade.DTO;
using Task_ASP.AppFacade.Mapping;
using Task_ASP.BL;
using Task_ASP.Library;

namespace Task_ASP.AppFacade
{
    public class ClientFacade : IClientFacade
    {
        private readonly IClientManager _clientManager;
        private readonly ILog _logger;

        public ClientFacade(IClientManager clientManager, ILog log)
        {
            this._clientManager = clientManager;
        }

        public IOperationResult AddClient(DTO_Client dtoClient)
        {
            return _clientManager.AddClient(dtoClient.ToManagerClient());
        }
        public IOperationResult RemoveClient(int clientID)
        {
            return _clientManager.RemoveClient(clientID);
        }
        public IOperationResult ModifyClient(DTO_Client dtoClient)
        {
            return _clientManager.ModifyClient(dtoClient.ToManagerClient());
        }


        public List<DTO_Client> Clients()
        {
            ClientNamesFilter filter = new ClientNamesFilter();
            var clientsList =
                (from
                    client in _clientManager.Clients()
                 where
                     filter.NameIsAllowed(client.Name)
                 select
                     client.ToFacadeClient()
                ).ToList();

            return clientsList;
        }

        public DTO_Client ClientByID(int clientID)
        {
            return _clientManager.ClientByID(clientID).ToFacadeClient();
        }

        public List<DTO_Order> ClientsOrders(int clientID)
        {
            return _clientManager.ClientsOrders(clientID).ToFacadeOrdersList();
        }
        public List<DTO_Order> ClientsOrders(int clientID, int startID, int ordersLimit)
        {
            return _clientManager.ClientsOrders(clientID, startID, ordersLimit).ToFacadeOrdersList();
        }
        public void ClientsOrdersPagingInfo(int clientID, out int firstID, out int lastID)
        {
            _clientManager.ClientsOrdersPagingInfo(clientID, out firstID, out lastID);
        }
    }

}
