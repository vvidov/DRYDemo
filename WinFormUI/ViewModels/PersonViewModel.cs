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
    public class PersonViewModel : ReactiveUI.ReactiveObject, IPersonModel

    {
        private string _firstName;
        private string _lastName;
        private string _employeeId;

        private IEmployeeProcessor _processor;
        public string FirstName { get => _firstName; set => this.RaiseAndSetIfChanged(ref _firstName, value); }
        public string LastName { get => _lastName; set => this.RaiseAndSetIfChanged(ref _lastName, value); }
        public string EmployeeId { get => _employeeId; set => this.RaiseAndSetIfChanged(ref _employeeId, value); }

        public ReactiveCommand GenerateIdCmd { get;}
        public ReactiveCommand GenerateIdCmdWithMessage { get; }
        public Interaction<string, bool> Confirm;

        public ReactiveCommand ClearCmd { get; }
        public ReactiveCommand ClearCmdWithMessage { get; }

        readonly ObservableAsPropertyHelper<string> _fullName;
        public string FullName => _fullName.Value;

        public PersonViewModel(IEmployeeProcessor processor)
        {
            _processor = processor;
            GenerateIdCmd = ReactiveCommand.Create(GenerateId);
            GenerateIdCmdWithMessage = ReactiveCommand.CreateFromTask(GenerateIdMess);
            ClearCmd = ReactiveCommand.Create(Clear, CanExecute());
            ClearCmdWithMessage = ReactiveCommand.CreateFromTask(ClearMess, CanExecute());
            Confirm = new Interaction<string, bool>();

            _fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName, (fn, ln) => $"{fn} {ln}")
                .ToProperty(this, x => x.FullName);
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
            EmployeeId = _processor.GenerateEmployeeID(_firstName, _lastName);
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
