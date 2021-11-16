using System.Collections.Generic;
using System.Linq;
using System.Text;
using INetApp.ViewModels.Base;

namespace INetApp.Validations
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private string _MsgErrors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                RaisePropertyChanged(() => this.Errors);
            }
        }
        public string MsgErrors
        {
            get => _MsgErrors;
            set
            {
                _MsgErrors = value;
                RaisePropertyChanged(() => this.MsgErrors);
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged(() => this.Value);
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => this.IsValid);
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            this.Errors.Clear();
            this.MsgErrors = "";

            IEnumerable<string> errors = _validations.Where(v => !v.Check(this.Value))
                .Select(v => v.ValidationMessage);

            this.Errors = errors.ToList();
            foreach (var item in errors)
            {
                this.MsgErrors += item +"\n";
            }
            
            this.IsValid = !this.Errors.Any();

            return this.IsValid;
        }
    }
}
