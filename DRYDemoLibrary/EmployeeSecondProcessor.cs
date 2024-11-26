using System;
using System.Linq;

namespace DRYDemoLibrary
{
    public class EmployeeSecondProcessor : IEmployeeProcessor
    {
        private string GetPartOfName(string name, int numberOfCharacters)
        {
            if (string.IsNullOrEmpty(name))
                return new string('Y', numberOfCharacters);

            string output = name;

            if (name.Length > numberOfCharacters)
            {
                output = name.Substring(0, numberOfCharacters);
            }
            else
            {
                while (output.Length < numberOfCharacters)
                {
                    output += "Y";
                }
            }

            return output;
        }

        public string GenerateEmployeeID(string firstName, string lastName)
        {
            // Test method for logging interceptor
            if (firstName.EndsWith("123"))
            {
                throw new DivideByZeroException("Test exception for logging");
            }

            return $"{GetPartOfName(lastName, 4)}_{GetPartOfName(firstName, 4)}_{DateTime.Now.Millisecond}";
        }

        public string GenerateEmployeeID2(string firstName, string lastName)
        {
            // Test method for logging interceptor
            if (firstName.EndsWith("123"))
            {
                throw new DivideByZeroException("Test exception for logging");
            }

            return $"{GetPartOfName(lastName, 4)}_{GetPartOfName(firstName, 4)}_{DateTime.Now.Millisecond}";
        }
    }
}
