namespace DRYDemoLibrary
{
    public interface IEmployeeProcessor
    {
        string GenerateEmployeeID(string firstName, string lastName);
        string GenerateEmployeeID2(string firstName, string lastName);
    }
}