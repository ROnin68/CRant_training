using System.Collections.Generic;

namespace Task_ASP.AppFacade.Mapping
{
    static class ClientMapper
    {
        /// <summary>
        /// BL.DTO.DTO_Client --> AppFacade.DTO.DTO_Client
        /// 
        /// </summary>
        /// <param name="blClient"></param>
        /// <returns></returns>
        public static DTO.DTO_Client ToFacadeClient(this BL.DTO.DTO_Client blClient)
        {
            return new DTO.DTO_Client()
            {
                ID = blClient.ID,
                Name = blClient.Name
            };
        }
        /// <summary>
        /// AppFacade.DTO.DTO_Client --> BL.DTO.DTO_Client
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <returns></returns>
        public static BL.DTO.DTO_Client ToManagerClient(this DTO.DTO_Client clientDTO)
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
                result.Add(cl.ToFacadeClient());
            }

            return result;
        }
    }
}
