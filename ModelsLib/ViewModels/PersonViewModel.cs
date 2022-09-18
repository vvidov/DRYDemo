using DRYDemoLibrary;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLib.Models;
using System.Windows.Forms;
using ModelsLib.Storage;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModelsLib.ViewModels
{
    public class PersonViewModel : ReactiveUI.ReactiveObject

    {
        private PersonModel Model;

        private IEmployeeProcessor _processor;
        public string FirstName { get => Model.FirstName; set => this.RaiseAndSetIfChanged(ref Model.FirstName, value); }
        public string LastName { get => Model.LastName; set => this.RaiseAndSetIfChanged(ref Model.LastName, value); }
        public string EmployeeId { get => Model.EmployeeId; set => this.RaiseAndSetIfChanged(ref Model.EmployeeId, value); }

        public ReactiveCommand GenerateIdCmd { get;}
        public ReactiveCommand GenerateIdCmdWithMessage { get; }
        public Interaction<MessageBoxInput, DialogResult> Confirm;

        public ReactiveCommand ClearCmd { get; }
        public ReactiveCommand ClearCmdWithMessage { get; }

        public ReactiveCommand SaveCmd { get; }
        public ReactiveCommand LoadCmd { get; }

        readonly ObservableAsPropertyHelper<string> _fullName;
        public string FullName => _fullName.Value;

        public PersonViewModel(IEmployeeProcessor processor, IModelStorage modelStorage)
        {
            _processor = processor;
            Model = new PersonModel(modelStorage);
            GenerateIdCmd = ReactiveCommand.Create(GenerateId);
            GenerateIdCmdWithMessage = ReactiveCommand.CreateFromTask(GenerateIdMess);
            ClearCmd = ReactiveCommand.Create(Clear, CanExecute());
            ClearCmdWithMessage = ReactiveCommand.CreateFromTask(ClearMess, CanExecute());
            Confirm = new Interaction<MessageBoxInput, DialogResult>();

            SaveCmd = ReactiveCommand.CreateFromTask(Save);
            LoadCmd = ReactiveCommand.CreateFromTask(Load);

            _fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName, (fn, ln) => $"{fn} {ln}")
                .ToProperty(this, x => x.FullName);
        }

        private async Task Load()
        {
            var message = "Load Saved Person?";

            var answer = await Confirm.Handle(new MessageBoxInput { Message = message });

            if (answer == DialogResult.Yes)
            {
                var model = Model.Load();
                FirstName = model.FirstName;
                LastName = model.LastName;
                EmployeeId = model.EmployeeId;
            }
        }

        private async Task Save()
        {
            var message = "Save Person to Storage?";

            var answer = await Confirm.Handle(new MessageBoxInput { Message = message });

            if (answer == DialogResult.Yes)
                Model.Save();
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
            EmployeeId = _processor.GenerateEmployeeID(FirstName, LastName);
        }

        public async Task GenerateIdMess()
        {
            var message = "Generate new Emp ID?";

            var answer = await Confirm.Handle(new MessageBoxInput { Message = message});

            if (answer == DialogResult.Yes)
                GenerateId();
            else
                EmployeeId = string.Empty;
        }
        public async Task ClearMess()
        {
            var message = "Clear all?";

            var answer = await Confirm.Handle(new MessageBoxInput { Message = message});

            if (answer == DialogResult.Yes)
                Clear();
        }
    }
}
