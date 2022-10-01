using DRYDemoLibrary;
using ModelsLib.Models;
using ModelsLib.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class DashboardRUI : Form, IViewFor<PersonViewModel>
    {
        public DashboardRUI(IEmployeeProcessor processor, IModelStorage modelStorage)
        {
            InitializeComponent();

            VM = new PersonViewModel(processor, modelStorage);

            // Bind the view to the ReactiveUI viewmodel
            this.WhenActivated(disposable =>
            {
                this.Bind(VM, vm => vm.FirstName, v => v.firstNameText.Text).DisposeWith(disposable);
                this.Bind(VM, wm => wm.LastName, v => v.lastNameText.Text).DisposeWith(disposable);
                this.OneWayBind(VM, wm => wm.EmployeeId, v => v.employeeIdText.Text).DisposeWith(disposable);

                this.BindCommand(VM, wm => wm.GenerateIdCmdWithMessage, v => v.generateEmployeeIdButton).DisposeWith(disposable);
                this.BindCommand(VM, wm => wm.ClearCmdWithMessage, v => v.btnClear).DisposeWith(disposable);
                this.BindCommand(VM, wm => wm.LoadCmd, v => v.btnLoad).DisposeWith(disposable);
                this.BindCommand(VM, wm => wm.SaveCmd, v => v.btnSave).DisposeWith(disposable);
            });
        }

        public PersonViewModel VM { get; set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (PersonViewModel)value; }
        }

        PersonViewModel IViewFor<PersonViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
    }
}
