using DRYDemoLibrary;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormUI.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormUI.ViewModels
{
    public class TestViewModel : ReactiveUI.ReactiveObject, ITestModel

    {
        private string firstName;
        private string lastName;
        private string employeeId;

        private IEmployeeProcessor _processor;

        public string FirstName { get => firstName; set => this.RaiseAndSetIfChanged(ref firstName, value); }
        public string LastName { get => lastName; set => this.RaiseAndSetIfChanged(ref lastName, value); }
        public string EmployeeId { get => employeeId; set => this.RaiseAndSetIfChanged(ref employeeId, value); }

        public ReactiveCommand GenerateIdCmd { get;}
        public ReactiveCommand GenerateIdCmdWithMessage { get; }
        public Interaction<string, bool> Confirm;

        public ReactiveCommand ClearCmd { get; }
        public ReactiveCommand ClearCmdWithMessage { get; }
        public TestViewModel(IEmployeeProcessor processor)
        {
            _processor = processor;
            GenerateIdCmd = ReactiveCommand.Create(GenerateId);
            GenerateIdCmdWithMessage = ReactiveCommand.CreateFromTask(GenerateIdMess);
            ClearCmd = ReactiveCommand.Create(Clear, CanExecute());
            ClearCmdWithMessage = ReactiveCommand.CreateFromTask(ClearMess, CanExecute());
            Confirm = new Interaction<string, bool>();
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(vm => vm.FirstName, vm => vm.LastName, (fn, ln) => !string.IsNullOrWhiteSpace(fn) || !string.IsNullOrWhiteSpace(ln));
        }

        private void Clear()
        {
            FirstName = String.Empty;
            LastName = String.Empty;
            EmployeeId = String.Empty;
        }

        private void GenerateId()
        {
            EmployeeId = _processor.GenerateEmployeeID(firstName, lastName);
        }

        public async Task GenerateIdMess()
        {
            var message = "Generate new Emp ID?";

            var answerYes = await Confirm.Handle(message);

            if (answerYes)
                GenerateId();
            else
                EmployeeId = string.Empty;
        }
        public async Task ClearMess()
        {
            var message = "Clear all?";

            var answerYes = await Confirm.Handle(message);

            if (answerYes)
                Clear();
        }
    }
}
