using DRYDemoLibrary;
using ModelsLib.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Di = System.Windows.MessageBox;
using ModelsLib;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<PersonViewModel>
    {
        private IEmployeeProcessor _processor;
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(PersonViewModel), typeof(MainWindow));
        public MainWindow(IEmployeeProcessor processor)
        {
            InitializeComponent();
            _processor = processor;

            ViewModel = new PersonViewModel(_processor);

            // Setup the bindings
            // Note: We have to use WhenActivated here, since we need to dispose the
            // bindings on XAML-based platforms, or else the bindings leak memory.
            this.WhenActivated(disposable =>
            {
                this.Bind(this.ViewModel, x => x.FirstName, x => x.firstName.Text).DisposeWith(disposable);
                this.Bind(this.ViewModel, x => x.LastName, x => x.lastName.Text).DisposeWith(disposable);
                this.OneWayBind(this.ViewModel, x => x.FullName, x => x.fullName.Text).DisposeWith(disposable);
                this.OneWayBind(this.ViewModel, x => x.EmployeeId, x => x.employeeId.Text).DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.GenerateIdCmdWithMessage, x => x.createEmployeeIdButton).DisposeWith(disposable);
                this.BindCommand(ViewModel, x => x.ClearCmdWithMessage, x => x.clearButton).DisposeWith(disposable);
            });
            this.WhenActivated(d =>
            {
                d(this
                    .ViewModel
                    .Confirm
                    .RegisterHandler(
                        async interaction =>
                        {
                            var res = await DisplayAlert(interaction.Input);

                            interaction.SetOutput(res);
                            //interaction.SetOutput(await Task.FromResult(MessageBox.Show(interaction.Input, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes));
                        }));

            });
        }

        private Task<DialogResult> DisplayAlert(MessageBoxInput messageBoxInput)
        {
            return Task.FromResult(System.Windows.Forms.MessageBox.Show(messageBoxInput.Message, messageBoxInput.Caption, messageBoxInput.Buttons, messageBoxInput.Icon));
        }

        public PersonViewModel ViewModel 
        {
            get => (PersonViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        object IViewFor.ViewModel 
        {
            get => ViewModel;
            set => ViewModel = (PersonViewModel)value;
        }

    }
}
