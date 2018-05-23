using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWebApi
{
    /// <summary>
    /// Model base class, used to house common methods
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Create comma-ified list of strings (typically used in spotify urls)
        /// </summary>
        /// <param name="strings"></param>
        /// <returns>comma-ified string</returns>
        public static string CreateCommaSeperatedList(List<string> strings)
        {
            var output = string.Empty;
            foreach (var str in strings)
                output += str + ",";
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        /// <summary>
        /// Create request string style key=value pairs
        /// </summary>
        /// <param name="strings"></param>
        /// <returns>comma-ified string</returns>
        public static string CreateKeyValueAmpersandSeperatedList(Dictionary<string, string> keyValuePairs)
        {
            var output = string.Empty;
            foreach (var str in keyValuePairs)
                output += str.Key + "=" + str.Value + "&";
            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
