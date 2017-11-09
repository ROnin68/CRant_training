using System.Collections.Generic;
using Task_ASP.AppFacade.DTO;
using Task_ASP.Web.Models;

namespace Task_ASP.Web.Mapping
{
    static class ClientMapper
    {
        public static ClientModel ToClientModel(this DTO.DTO_Client dtoClient)
        {
            return new ClientModel() {
                ID = dtoClient.ID,
                Name = dtoClient.Name
            };
        }

        public static DTO.DTO_Client ToDTO_Client(this ClientModel clientModel)
        {
            return new DTO.DTO_Client() {
                ID = clientModel.ID,
                Name = clientModel.Name
            };
        }


        public static ClientListModel CreateClientsListModel(this IClientFacade facade) 
        {
            List<ClientModel> modelList = new List<ClientModel>();
            foreach (var cl in facade.GetClients()) 
            {
                modelList.Add(cl.ToClientModel());
            }

            return new ClientListModel(modelList);
        }
    }
}
