using System.Text.RegularExpressions;

namespace Task_ASP.AppFacade
{
    public class DisplyNamesCensor
    {
        public bool DisplayNameAllowed(string name)
        {
            Match match = Regex.Match(name, "[<>#]", RegexOptions.IgnoreCase);

            return !match.Success;
        }
    }
}
