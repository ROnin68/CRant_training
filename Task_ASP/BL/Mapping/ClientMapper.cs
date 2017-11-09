using System.Collections.Generic;
using Task_ASP.BL.DTO;
using Task_ASP.DAL.Entities;

namespace Task_ASP.BL.Mapping
{
    static class ClientMapper
    {
        /// <summary>
        /// DAL.Entities.Client --> BL.DTO.DTO_Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static DTO_Client ToManagerDTO_Client(this Client client)
        {
            return new DTO_Client()
            {
                ID = client.ID,
                Name = client.Name
            };
        }
        /// <summary>
        /// BL.DTO.DTO_Client --> DAL.Entities.Client
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <returns></returns>
        public static Client ToClient(this DTO_Client clientDTO)
        {
            return new Client()
            {
                ID = clientDTO.ID,
                Name = clientDTO.Name
            };
        }

        public static List<DTO_Client> ToDTOClientsList(this List<Client> clientsList)
        {
            var result = new List<DTO_Client>();
            foreach (var cl in clientsList)
            {
                result.Add(cl.ToManagerDTO_Client());
            }

            return result;
        }
    }
}
