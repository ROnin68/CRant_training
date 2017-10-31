using System.Text.RegularExpressions;

namespace Task_ASP.AppFacade
{
    public class ClientNamesFilter
    {
        public bool NameIsAllowed(string name)
        {
            Match match = Regex.Match(name, "[<>#]", RegexOptions.IgnoreCase);

            return !match.Success;
        }
    }
}
