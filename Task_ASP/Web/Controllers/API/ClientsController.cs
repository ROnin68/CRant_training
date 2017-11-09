using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Task_ASP.AppFacade;
using Task_ASP.AppFacade.DTO;
using Task_ASP.Web.Mapper.API;
using Task_ASP.Web.Models.API;

namespace Task_ASP.Web.Controllers.API
{
    public class ClientsController : ApiController
    {
        private const int OrdersPerPage = 10;
        private readonly IClientFacade clientFacade;

        public ClientsController(IClientFacade clientFacade)
        {
            this.clientFacade = clientFacade;
        }

        // GET: api/Clients
        public IEnumerable<ClientDTO> Get()
        {
            List<ClientDTO> clientList = clientFacade.Clients();
            return clientList;
        }

        // GET: api/Clients/5
        public ClientModel Get(int ID)
        {
            var client = clientFacade.ClientByID(ID);

            return new ClientModel {
                ID = client.ID,
                Name = client.Name,
                Orders = $"http://127.0.0.1/api/clients/{client.ID}/orders"
            };
        }

        // GET: api/Clients/5/Orders?idlt=723
        // GET: api/Clients/5/Orders?idgt=725
        public OrdersListModel Get(int clientID, string operation, int idgt = 0)
        {
            if (operation == "orders") {
                List<DTO_Order> ordersList = clientFacade.ClientsOrders(clientID, idgt, OrdersPerPage);
                int firstID;
                int lastID;
                clientFacade.ClientsOrdersPagingInfo(clientID, out int firstID, out int lastID);
                OrdersListModel ordersListModel = ordersList.ToOrderListModel(fistID, lastID);
                return ordersListModel;
            }

            return null;
        }

        // POST: api/Clients
        public void Post([FromBody]DTO.DTO_Client value)
        {
            clientFacade.AddClient(value);
        }

        // PUT: api/Clients/5
        public void Put(int ID, [FromBody]DTO.DTO_Client value)
        {
            if (value.ID == ID) {
                clientFacade.ModifyClient(value);
            }
        }

        // DELETE: api/Clients/5
        public void Delete(int ID)
        {
            clientFacade.RemoveClient(ID);
        }
    }
}
