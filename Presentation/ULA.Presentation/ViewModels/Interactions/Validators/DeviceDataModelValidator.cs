using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using FluentValidation;
using ULA.Localization;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.ViewModels.Interactions.Entities;

namespace ULA.Presentation.ViewModels.Interactions.Validators
{
    /// <summary>
    ///     Represents a validation rule sets registry for an instance of <see cref="IDeviceDataModel" />
    ///     Представляет набор валидационных правил для модели данных устройства
    /// </summary>
    public class DeviceDataModelValidator : AbstractValidator<IModifyDeviceViewModel>
    {
        private IEnumerable<string> _ipsExisting;
        private IEnumerable<string> _deviceNamesExisting;
        private IModifyDeviceViewModel _model;

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DeviceDataModelValidator" />
        /// </summary>
        public DeviceDataModelValidator()
        {
            this.RuleFor(p => p.DeviceName)
                .NotEmpty()
                .WithMessage(LocalizationResources.Instance.DeviceNameValidationNotSet)
                .Length(2, 20)
                .WithMessage(LocalizationResources.Instance.DeviceNameValidationRange);
            this.RuleFor(p => p.TcpIpAddress)
                .NotEmpty()
                .WithMessage(LocalizationResources.Instance.IpAddressValidationNotSet)
                .Matches(
                    @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$",
                    RegexOptions.Compiled)
                .WithMessage(LocalizationResources.Instance.IpAddressValidationFormat);
            this.RuleFor(p => p.TcpIpPort)
                .NotEmpty()
                .WithMessage(LocalizationResources.Instance.IpPortValidationNotSet)
                .Length(1, 4)
                .WithMessage(LocalizationResources.Instance.IpPortValidationRange)
                .Matches(@"^[0-9]{0,4}$", RegexOptions.Compiled)
                .WithMessage(LocalizationResources.Instance.IpPortValidationFormat);
            RuleFor(p => p.DeviceType).NotEqual("UNKNOWN").WithMessage("Выберите тип устройства");
            RuleFor(p => p.TransformKoefA)
                .NotEmpty()
                .WithMessage("Установите первичный ток трансформатора для МСА961")
                .Matches(@"^\d+$", RegexOptions.Compiled).WithMessage("Значение должно быть целым числом");
            RuleFor(p => p.TransformKoefB)
                .NotEmpty()
                .WithMessage("Установите первичный ток трансформатора для МСА961")
                .Matches(@"^\d+$", RegexOptions.Compiled).WithMessage("Значение должно быть целым числом");
            RuleFor(p => p.TransformKoefC)
                .NotEmpty()
                .WithMessage("Установите первичный ток трансформатора для МСА961")
                .Matches(@"^\d+$", RegexOptions.Compiled).WithMessage("Значение должно быть целым числом");
            RuleFor(p => (p as ModifyDeviceInteractionViewModel).StarterDescriptionsViewModels)
                .Must((models =>
                {
                    var groupsNum = models.GroupBy((model => model.ChannelNumberOfStarter)).Count();
                    if (models.Any((model => model.ChannelNumberOfStarter == 0)))
                    {
                        var emptyNum = models.Where((model => model.ChannelNumberOfStarter == 0)).Count() - 1;
                        bool isValidWithEmpty = groupsNum + emptyNum == 3;
                        return isValidWithEmpty;
                    }
                    bool isValid = groupsNum
                               == 3;
                    return isValid;
                }))
                .WithMessage("Номера каналов заданы неверно");


            RuleFor(p => p.IndicatorDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsIndicatorsEnabled) return true;
                return models.GroupBy(x => x.Description).All(g => g.Count() == 1);
            })).WithMessage("Описания индикаторов должны отличаться");
            RuleFor(p => p.IndicatorDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsIndicatorsEnabled) return true;
                return !models.Any((model => model.Description == String.Empty));
            })).WithMessage("Описания индикаторов должны быть непустыми");





            RuleFor(p => p.CascadeDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsCascadeEnabled) return true;
                return !models.Any((model =>model.Description==String.Empty ));
            })).WithMessage("Описания каскада не должны быть непустыми");
            RuleFor(p => p.CascadeDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsCascadeEnabled) return true;
                return models.GroupBy(x => x.Description).All(g => g.Count() == 1);
            })).WithMessage("Описания каскада должны отличаться");






            RuleFor(p => p.SignalDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsSignalsEnabled) return true;
                return !models.Any((model => model.Description == String.Empty));
            })).WithMessage("Описания сигналов должны быть непустыми");
            RuleFor(p => p.SignalDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsSignalsEnabled) return true;
                return models.GroupBy(x => x.Description).All(g => g.Count() == 1);
            })).WithMessage("Описания сигналов должны отличаться");






            RuleFor(p => p.FiderDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsFiderOrganization) return true;
                return models.GroupBy(x => x.TagName).All(g => g.Count() == 1);
            })).WithMessage("Описания фидеров должны отличаться");
            RuleFor(p => p.FiderDefinitionsViewModels).Must((models =>
            {
                if (!_model.IsFiderOrganization) return true;
                return !models.Any((model => model.TagName == String.Empty));
            })).WithMessage("Описания фидеров должны быть непустыми");








            RuleFor(p => p.DeviceName).Must((name =>
            {
                return !_deviceNamesExisting.Any((s => s.Equals(name)));
            })).WithMessage("Такое имя устройства уже используется");

            RuleFor(p => p.TcpIpAddress).Must((ip =>
            {
                return !_ipsExisting.Any((s => s.Equals(ip)));
            })).WithMessage("Такой IP-адрес устройства уже используется");

        }

     

        public void SetContext(IEnumerable<string> ipsExisting, IEnumerable<string> deviceNamesExisting, IModifyDeviceViewModel model)
        {
            _ipsExisting = ipsExisting;
            _model = model;
            _deviceNamesExisting = deviceNamesExisting;
        }


        #endregion [Ctor's]
    }
}