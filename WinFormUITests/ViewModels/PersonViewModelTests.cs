using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DRYDemoLibrary;
using ModelsLib.ViewModels;

namespace ModelsLib.ViewModels.Tests
{
    public class PersonViewModelTests
    {

        [Theory]
        [InlineData("Firstname", "LastName", true)]
        public async Task GenerateIdMess_ShouldGenerate(string firstName, string lastName, bool generate)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(generate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;
            var Id = processor.GenerateEmployeeID(firstName, lastName);

            await fixture.GenerateIdMess();


            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(Id, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", false)]
        public async Task GenerateIdMess_ShouldNotGenerate(string firstName, string lastName, bool generate)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(generate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();


            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(String.Empty, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", true, true)]
        public async Task ClearMess_ShouldClear(string firstName, string lastName, bool generate, bool clear)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(generate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();

            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(clear));

            await fixture.ClearMess();

            Assert.Equal(string.Empty, fixture.FirstName);
            Assert.Equal(string.Empty, fixture.LastName);
            Assert.Equal(string.Empty, fixture.EmployeeId);
        }

        [Theory]
        [InlineData("Firstname", "LastName", true, false)]
        public async Task ClearMess_ShouldNotClear(string firstName, string lastName, bool generate, bool clear)
        {
            IEmployeeProcessor processor = new EmployeeProcessor();
            var fixture = new PersonViewModel(processor);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(generate));


            fixture.FirstName = firstName;
            fixture.LastName = lastName;

            await fixture.GenerateIdMess();

            var Id = processor.GenerateEmployeeID(firstName, lastName);
            fixture.Confirm
                .RegisterHandler(interaction => interaction.SetOutput(clear));

            await fixture.ClearMess();

            Assert.Equal(firstName, fixture.FirstName);
            Assert.Equal(lastName, fixture.LastName);
            Assert.Equal(Id, fixture.EmployeeId);
        }
    }
}