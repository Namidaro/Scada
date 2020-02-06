using System;
using System.Collections.Generic;
using System.Globalization;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.AddinsHost.Business.Device.Context
{
    /// <summary>
    ///     Represents device context entity in AOM notation builder functionality
    ///     Представляет строитель контекста сущности устройства в AOM нотации (см. AOM паттерн)
    /// </summary>
    public class AomDeviceContextEntityBuilder
    {
        #region [Const]
        private const string TIMEOUT_PERIOD_NAME = "TimeoutPeriod";
        #endregion

        #region [Private fields]

        private string _deviceDescription;
        private string _deviceName;
        private string _deviceType;
        private string _driverId;
        private string _analogMeterType;
        private List<ICustomFider> _customFiders;
        private List<ICustomSignal> _customSignals;
        private List<ICustomIndicator> _customIndicators;
        private List<ICascadeIndicator> _cascadeIndicators;
        private int _transformKoefA;
        private int _transformKoefB;
        private int _transformKoefC;


        #endregion [Private fields]

        #region [Public members]

        /// <summary>
        ///     Sets the name of the device
        ///     Устанавливает имя устройства
        /// </summary>
        /// <param name="deviceName">The name of the device</param>
        public void SetDeviceName(string deviceName)
        {
            if (deviceName == null) throw new ArgumentNullException("deviceName");
            this._deviceName = deviceName;
        }

        /// <summary>
        ///     Sets the description of the device
        ///     Устанавливает описание устройства
        /// </summary>
        /// <param name="deviceDescription">The device description</param>
        public void SetDeviceDescription(string deviceDescription)
        {
            this._deviceDescription = deviceDescription;
        }

        /// <summary>
        ///     Sets the type of the device
        ///     Устанавливает тип устройства
        /// </summary>
        /// <param name="deviceType">The type of the device</param>
        public void SetDeviceType(string deviceType)
        {
            if (deviceType == null) throw new ArgumentNullException("deviceType");
            this._deviceType = deviceType;
        }


        /// <summary>
        ///     Sets the type of the device
        ///     Устанавливает тип устройства
        /// </summary>
        /// <param name="analogMeterType">The type of the device</param>
        public void SeAnalogMeterType(string analogMeterType)
        {
            if (analogMeterType == null) throw new ArgumentNullException("analogMeterType");
            _analogMeterType = analogMeterType;
        }

        /// <summary>
        ///     Sets the type of the device
        ///     Устанавливает тип устройства
        /// </summary>
        public void SetAnalogMeterKoefs(int transformKoefA, int transformKoefB, int transformKoefC)
        {
            _transformKoefA = transformKoefA;
            _transformKoefB = transformKoefB;
            _transformKoefC = transformKoefC;

        }

        /// <summary>
        ///     Устанавливает фидеры
        /// </summary>
        /// <param name="customFiders"></param>
        public void SetCustomFiders(List<ICustomFider> customFiders)
        {
            if (customFiders == null) throw new ArgumentNullException("customFiders");
            _customFiders = customFiders;

        }

        /// <summary>
        ///     Устанавливает фидеры
        /// </summary>
        /// <param name="customSignals"></param>
        public void SetCustomSignals(List<ICustomSignal> customSignals)
        {
            if (customSignals == null) throw new ArgumentNullException("customSignals");
            _customSignals = customSignals;

        }




        /// <summary>
        ///     Устанавливает фидеры
        /// </summary>
        /// <param name="customFiders"></param>
        public void SetCustomIndicators(List<ICustomIndicator> customIndicators)
        {
            if (customIndicators == null) throw new ArgumentNullException("customIndicators");
            _customIndicators = customIndicators;

        }


        /// <summary>
        ///     Устанавливает фидеры
        /// </summary>
        /// <param name="cascadeIndicators"></param>
        public void SetCustomCascade(List<ICascadeIndicator> cascadeIndicators)
        {
            if (cascadeIndicators == null) throw new ArgumentNullException("cascadeIndicators");
            _cascadeIndicators = cascadeIndicators;

        }


        /// <summary>
        ///     Sets the unique identifier of the driver with which current device is associated
        ///     Устанавливает id устройства
        /// </summary>
        /// <param name="driverId"></param>
        public void SetDriverId(string driverId)
        {
            this._driverId = driverId;
        }

        /// <summary>
        ///     Builds an instance of <see cref="AomDeviceContextEntity" />
        ///     Строит AOM контекст сущность (см. AOM паттерн)
        /// </summary>
        /// <returns>
        ///     Built instance of <see cref="AomDeviceContextEntity" />
        /// </returns>
        public AomDeviceContextEntity Build()
        {
            var result = new AomDeviceContextEntity
                {
                    DeviceName = this._deviceName,
                    DeviceType = this._deviceType,
                    DeviceDescription = this._deviceDescription,
                    AnalogMeterType = _analogMeterType,
                    RelatedDriverId = _driverId,
                TransformKoefA = _transformKoefA,
                    TransformKoefB = _transformKoefB,
                    TransformKoefC = _transformKoefC,

            };
            result.CustomDeviceSchemeTable=new CustomDeviceSchemeTable();
            if (_customFiders.Count > 0)
            {
             _customFiders.ForEach((fider => result.CustomDeviceSchemeTable.FidersTable.AddObject(fider)));   
            }
            if (_customIndicators.Count > 0)
            {
                _customIndicators.ForEach((ind => result.CustomDeviceSchemeTable.IndicatorsTable.AddObject(ind)));
            }
            if (_customSignals.Count > 0)
            {
                _customSignals.ForEach((signal => result.CustomDeviceSchemeTable.SignalsTable.AddObject(signal)));
            }
            if (_cascadeIndicators.Count > 0)
            {
                _cascadeIndicators.ForEach((cascade => result.CustomDeviceSchemeTable.CascadeIndicatorsTable.AddObject(cascade)));
            }


            return result;
        }

        #endregion [Public members]
    }
}