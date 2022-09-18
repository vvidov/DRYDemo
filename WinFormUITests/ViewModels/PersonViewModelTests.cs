using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DRYDemoLibrary;
using ModelsLib.ViewModels;
using System.Windows.Forms;

namespace ModelsLib.ViewModels.Tests
{
    public class PersonViewModelTests
    {

        [Theory]
        [InlineData("Firstname", "LastName", DialogResult.Yes)]
        public async Task GenerateIdMess_ShouldGenerate(string firstName, string lastName, DialogResult dlgResult)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgResult));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;
            var Id = processor.GenerateEmployeeID(firstName, lastName);

            await fixture.GenerateIdMess();


            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(Id, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", DialogResult.Cancel)]
        [InlineData("Firstname", "LastName", DialogResult.OK)]
        [InlineData("Firstname", "LastName", DialogResult.No)]
        public async Task GenerateIdMess_ShouldNotGenerate(string firstName, string lastName, DialogResult dlgResult)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgResult));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();


            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(String.Empty, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", DialogResult.Yes, DialogResult.Yes)]
        public async Task ClearMess_ShouldClear(string firstName, string lastName, DialogResult dlgCreate, DialogResult dlgClear)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgCreate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();

            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgClear));

            await fixture.ClearMess();

            Assert.Equal(string.Empty, fixture.FirstName);
            Assert.Equal(string.Empty, fixture.LastName);
            Assert.Equal(string.Empty, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", DialogResult.Yes, DialogResult.No)]
        [InlineData("Firstname", "LastName", DialogResult.Yes, DialogResult.Cancel)]
        [InlineData("Firstname", "LastName", DialogResult.Yes, DialogResult.Ignore)]
        public async Task ClearMess_ShouldNotClear(string firstName, string lastName, DialogResult dlgGenerate, DialogResult dlgClear)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgGenerate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();

            var Id = processor.GenerateEmployeeID(firstName, lastName);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(dlgClear));

            await fixture.ClearMess();

            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(Id, fixture.EmployeeId);
        }
    }
}