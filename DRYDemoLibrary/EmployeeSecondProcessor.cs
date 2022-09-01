using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRYDemoLibrary
{
    public class EmployeeSecondProcessor : IEmployeeProcessor
    {
        public string GenerateEmployeeID(string firstName, string lastName)
        {
            if (firstName.EndsWith("123"))
            {
                var zero = 0;
                var asd = 123 / zero;
            }
            string employeeId = $@"{GetPartOfName(lastName, 4)}_{GetPartOfName(firstName, 4)}_{DateTime.Now.Millisecond}";
            return employeeId;
        }

        public string GenerateEmployeeID2(string firstName, string lastName)
        {
            if (firstName.EndsWith("123"))
            {
                var zero = 0;
                var asd = 123 / zero;
            }
            string employeeId = $@"{GetPartOfName(lastName, 4)}_{GetPartOfName(firstName, 4)}_{DateTime.Now.Millisecond}";
            return employeeId;
        }

        private string GetPartOfName(string name, int numberOfCharacters)
        {
            string output = name;

            if (name.Length > numberOfCharacters)
            {
                output = name.Substring(0, numberOfCharacters);
            }
            else
            {
                do
                {
                    output += "Y";
                } while (output.Length < 4);
            }

            return output;
        }
    }
}
