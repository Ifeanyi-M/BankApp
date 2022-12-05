using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYBANKAPP.UI
{
    public static class Validator
    {
        public static T Transform<T>(string prompt)
        {
            bool valid = false;
            string userInput;

            while (!valid)
            {
                userInput = Service.GetUserDeets(prompt);

                try
                {
                    var transformer = TypeDescriptor.GetConverter(typeof(T));
                    if (transformer != null)
                    {
                        return (T)transformer.ConvertFromString(userInput);
                    }
                    else
                    {
                        return default;
                    }
                }
                catch
                {
                    Service.PrintMessage("Invalid Entry. Retry!", false);
                }
            }
            return default;
        }
    }
}
