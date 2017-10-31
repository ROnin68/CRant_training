using System.Collections.Generic;
using Task_ASP.AppFacade.DTO;
using Task_ASP.Library;

namespace Task_ASP.AppFacade
{
    public interface IClientFacade
    {
        IOperationResult AddClient(DTO_Client client);
        IOperationResult RemoveClient(int clientID);
        IOperationResult ModifyClient(DTO_Client client);
        List<DTO_Client> Clients();
        DTO_Client ClientByID(int clientID);
    }
}