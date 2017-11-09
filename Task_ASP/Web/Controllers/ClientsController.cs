using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_ASP.ApplFacade;
using Task_ASP.AppFacade.DTO;
using Task_ASP.AppFacade.Mapper;
using Task_ASP.Library;
using Task_ASP.Web.Models;

namespace Task_ASP.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientFacade clientFacade;

        public ClientsController(IClientFacade clientFacade)
        {
            this.clientFacade = clientFacade;
        }

        // GET: Client
        public ActionResult Index()
        {
            ClientListModel clientList = clientFacade.CreateClientsListModel();
            return View(clientList);
        }

        public ActionResult ClientAddForm()
        {
            return View(new ClientModel());
        }

        public ActionResult ClientModifyForm(int clientID)
        {
            ClientModel client = clientFacade.GetClientByID(clientID).ToClientModel();
            return View(client);
        }

        // GET: Client
        public ActionResult ManageClients()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientModify(ClientModel clientModel)
        {
            IResultMessage result = clientFacade.ClientModify(clientModel.ToClientDTO());

            ClientListModel clientList = clientFacade.CreateClientsListModel();
            clientList.OperationResult = result;

            return View("Index", clientList);
        }

        [HttpPost]
        public ActionResult ClientRemove(int clientID)
        {
            IResultMessage result = clientFacade.ClientRemove(clientID);
            ClientListModel clientList = clientFacade.CreateClientsListModel();
            clientList.OperationResult = result;

            return View("Index", clientList);
        }

        [HttpPost]
        public ActionResult ClientAdd(ClientModel clientModel)
        {
            IResultMessage result = clientFacade.ClientAdd(clientModel.ToClientDTO());

            ClientListModel clientList = clientFacade.CreateClientsListModel();
            clientList.OperationResult = result;

            return View("Index", clientList);
        }


    }
}