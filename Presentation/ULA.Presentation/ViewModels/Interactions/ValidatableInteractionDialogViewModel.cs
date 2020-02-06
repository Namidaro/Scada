using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Представляет базовую вью-модель интерактивного окна с валидацией
    /// </summary>
    public abstract class ValidatableInteractionDialogViewModel<TValidationModel> : InteractionDialogViewModel, IDataErrorInfo
        where TValidationModel : class
    {
        #region [Private fields]

        private bool _validating;
        private IDictionary<string, IEnumerable<ValidationFailure>> _validationResults;

        #endregion [Private fields]

        #region [Templated members]

        /// <summary>
        ///     Submits interaction request
        ///     Запрос на подтверждение
        /// </summary>
        protected override void OnSubmit()
        {
            if (!this.IsValid()) return;
            this.OnSubmitting();
            this.IsCanceled = false;
            base.OnSubmit();
        }

        /// <summary>
        ///     Validating current interaction dialog
        ///     Валидация текущего интерактивного диалога
        /// </summary>
        /// <param name="validator">
        ///     An instance of <see cref="AbstractValidator{T}" /> to use for validation
        /// </param>
        protected virtual ValidationResult OnValidate(AbstractValidator<TValidationModel> validator)
        {
            if (validator == null)
                return new ValidationResult();
            var model = this.GetValidationModel();
            return model == null ? new ValidationResult() : validator.Validate(model);
        }

        /// <summary>
        ///     Gets current view model validator mechanism
        ///     Получает текущий механиз валидации вью-модели
        /// </summary>
        /// <returns>
        ///     Resulted instance of <see cref="AbstractValidator{T}" />
        /// </returns>
        protected abstract AbstractValidator<TValidationModel> GetValidator();

        /// <summary>
        ///     Gets an instance of validation model
        ///     Получает сущность для валидации
        /// </summary>
        /// <returns>Resulted instance of a validation model</returns>
        protected abstract TValidationModel GetValidationModel();

        #endregion [Templated members]

        #region [IDataErrorInfo members]

        /// <summary>
        ///     Gets the error message for the property with the given name.
        ///     Получает описание ошибки по её имени
        /// </summary>
        /// <returns>
        ///     The error message for the property. The default is an empty string ("").
        /// </returns>
        /// <param name="columnName">The name of the property whose error message to get. </param>
        public string this[string columnName]
        {
            get
            {
                var result = string.Empty;
                if (!this._validating) return result;
                if (this._validationResults.Count == 0) return result;
                IEnumerable<ValidationFailure> valResult;
                return this._validationResults.TryGetValue(columnName, out valResult)
                           ? string.Join("; ", valResult.Select(s => s.ErrorMessage))
                           : result;
            }
        }

        /// <summary>
        ///     Gets an error message indicating what is wrong with this object.
        ///     Получает описание, что не так с этим объектом
        /// </summary>
        /// <returns>
        ///     An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public string Error
        {
            get { return string.Empty; }
        }

        #endregion [IDataErrorInfo members]

        #region [Help members]

        //Метод возвращающий состояние валидности этого объекта
        private bool IsValid()
        {
            this._validationResults = null;
            this._validating = true;
            var validator = this.GetValidator();
            var result = this.OnValidate(validator);
            if (result.IsValid)
            {
                this._validating = false;
                return true;
            }
            this._validationResults = new Dictionary<string, IEnumerable<ValidationFailure>>();
            foreach (var group in result.Errors.GroupBy(g => g.PropertyName))
                this._validationResults.Add(group.Key, group);
            this.OnPropertyChanged("");
            this._validating = false;
            return result.IsValid;
        }

        #endregion [Help members]
    }
}