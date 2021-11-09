namespace Com.Gmail.Birklid.Ray.Tipsy.ViewModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Controllers;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Services.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class PersonDialogViewModel : BindableBase, IDialogAware, IDataErrorInfo
    {
        #region Private Fields

        private readonly DelegateCommand _cancelCommand;
        private readonly IDataController _dataController;
        private string[] _invalidNames;
        private string _name;
        private string _nameError;
        private readonly DelegateCommand _okCommand;
        private string _originalName;
        private string _title = "Person";

        #endregion Private Fields

        #region Creation

        public PersonDialogViewModel(
            IDataController dataController)
        {
            Log.Created(this);
            _dataController = dataController;
            _cancelCommand = new DelegateCommand(OnCancel);
            _okCommand = new DelegateCommand(OnOk, CanOk);
        }

        #endregion Creation

        public ICommand CancelCommand => _cancelCommand;
        
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Validate();
                    RaisePropertyChanged();
                }
            }
        }
        
        public ICommand OkCommand => _okCommand;

        #region IDialogAware Members

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(
            IDialogParameters parameters)
        {
            _originalName = parameters.GetValue<string>("_originalName") ?? string.Empty;
            _invalidNames = _dataController.People.Select(e => e.Name).Where(e => e != _originalName).ToArray();
            Name = _originalName;
        }

        #endregion IDialogAware Members

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error => string.Empty;
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return columnName == "Name" ? _nameError : null;
            }
        }

        #endregion IDataErrorInfo Members

        private Logger Log => ViewModelTraceSource.Instance;

        private void CloseDialog(
            bool dialogResult)
        {
            ButtonResult buttonResult = dialogResult ? ButtonResult.OK : ButtonResult.Cancel;
            var parameters = new DialogParameters($"_name={_name}");
            RequestClose?.Invoke(new DialogResult(buttonResult, parameters));
        }

        private bool CanOk()
        {
            return string.IsNullOrWhiteSpace(_nameError);
        }

        private void OnCancel()
        {
            Log.MethodCall(this);
            CloseDialog(false);
        }

        private void OnOk()
        {
            Log.MethodCall(this);
            CloseDialog(true);
        }

        private void RaiseRequestClose(
            IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                _nameError = "A value is required";
            }
            else if (_invalidNames.Contains(_name, StringComparer.OrdinalIgnoreCase))
            {
                _nameError = "That name is already in use";
            }
            else
            {
                _nameError = string.Empty;
            }
            _okCommand.RaiseCanExecuteChanged();
        }
    }
}
