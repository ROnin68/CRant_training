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
            return _clientManager.AddClient(dtoClient.ToManagerDTOClient());
        }
        public IOperationResult RemoveClient(int clientID)
        {
            return _clientManager.RemoveClient(clientID);
        }
        public IOperationResult ModifyClient(DTO_Client dtoClient)
        {
            return _clientManager.ModifyClient(dtoClient.ToManagerDTOClient());
        }


        public List<DTO_Client> Clients()
        {
            DisplyNamesCensor filter = new DisplyNamesCensor();
            var clientsList =
                (from
                    client in _clientManager.Clients()
                 where
                     filter.DisplayNameAllowed(client.Name)
                 select
                     client.ToFacadeDTOClient()
                ).ToList();

            return clientsList;
        }

        public DTO_Client ClientByID(int clientID)
        {
            return _clientManager.ClientByID(clientID).ToFacadeDTOClient();
        }
    }
}
