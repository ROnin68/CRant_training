using System.Collections.Generic;

namespace Task_ASP.AppFacade.Mapping
{
    static class ClientMapper
    {
        public static DTO.DTO_Client ToFacadeDTOClient(this BL.DTO.DTO_Client clientBL)
        {
            return new DTO.DTO_Client()
            {
                ID = clientBL.ID,
                Name = clientBL.Name
            };
        }

        public static BL.DTO.DTO_Client ToManagerDTOClient(this DTO.DTO_Client clientDTO)
        {
            return new BL.DTO.DTO_Client()
            {
                ID = clientDTO.ID,
                Name = clientDTO.Name
            };
        }


        public static List<DTO.DTO_Client> ToFacadeClientsList(this List<BL.DTO.DTO_Client> clList)
        {
            var result = new List<DTO.DTO_Client>();
            foreach (var cl in clList)
            {
                result.Add(cl.ToFacadeDTOClient());
            }

            return result;
        }
    }
}
