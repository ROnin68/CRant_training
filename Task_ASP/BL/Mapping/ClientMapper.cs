using System.Collections.Generic;
using Task_ASP.BL.DTO;
using Task_ASP.DAL.Entities;

namespace Task_ASP.BL.Mapping
{
    static class Client_DTOMapper
    {
        public static DTO_Client ToDTO_Client(this Client client)
        {
            return new DTO_Client()
            {
                ID = client.ID,
                Name = client.Name
            };
        }

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
                result.Add(cl.ToDTO_Client());
            }

            return result;
        }
    }
}
