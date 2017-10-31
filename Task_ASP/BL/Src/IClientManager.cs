﻿using System.Collections.Generic;
using Task_ASP.BL.DTO;
using Task_ASP.Library;

namespace Task_ASP.BL
{
    public interface IClientManager
    {
        IOperationResult AddClient(DTO_Client clientDTO);
        IOperationResult RemoveClient(int clientID);
        IOperationResult ModifyClient(DTO_Client clientDTO);
        DTO_Client ClientByID(int clientID);
        List<DTO_Client> Clients();
    }
}