using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_ASP.AppFacade.DTO;
using Task_ASP.Library;

namespace Task_ASP.Web.Models
{
    public class ClientListModel
    {
        private readonly List<ClientModel> _list;

        public ClientListModel(List<ClientModel> list)
        {
            this._list = list;
        }

        public List<ClientModel> Clients() {
            return _list;
        }

        public IOperationResult OperationResult { get; set; }
    }
}