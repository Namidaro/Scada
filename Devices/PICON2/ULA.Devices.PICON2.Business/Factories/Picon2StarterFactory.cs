using System;
using System.Collections.Generic;
using NLog;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors;
using ULA.Interceptors.IoC;

namespace ULA.Devices.PICON2.Business.Factories
{
    public class Picon2StarterFactory : IStarterFactory
    {
        private readonly IIoCContainer _container;

        private const string MANAGEMENT_KU_DISCRETE_1 = "Модуль 1 Д №1";
        private const string MANAGEMENT_KU_DISCRETE_2 = "Модуль 1 Д №2";
        private const string MANAGEMENT_KU_DISCRETE_3 = "Модуль 1 Д №3";
        private const string MANAGEMENT_KU_DISCRETE_4 = "Модуль 1 Д №4";
        private const string MANAGEMENT_KU_DISCRETE_5 = "Модуль 1 Д №5";
        private const string MANAGEMENT_KU_DISCRETE_6 = "Модуль 1 Д №6";
        private const string MANAGEMENT_KU_DISCRETE_7 = "Модуль 1 Д №7";
        private const string MANAGEMENT_KU_DISCRETE_8 = "Модуль 1 Д №8";


        private const string MANAGEMENT_KU_DISCRETE_21 = "Модуль 2 Д №1";
        private const string MANAGEMENT_KU_DISCRETE_22 = "Модуль 2 Д №2";
        private const string MANAGEMENT_KU_DISCRETE_23 = "Модуль 2 Д №3";
        private const string MANAGEMENT_KU_DISCRETE_24 = "Модуль 2 Д №4";
        private const string MANAGEMENT_KU_DISCRETE_25 = "Модуль 2 Д №5";
        private const string MANAGEMENT_KU_DISCRETE_26 = "Модуль 2 Д №6";
        private const string MANAGEMENT_KU_DISCRETE_27 = "Модуль 2 Д №7";
        private const string MANAGEMENT_KU_DISCRETE_28 = "Модуль 2 Д №8";


        private const string MANAGEMENT_KU_DISCRETE_31 = "Модуль 3 Д №1";
        private const string MANAGEMENT_KU_DISCRETE_32 = "Модуль 3 Д №2";
        private const string MANAGEMENT_KU_DISCRETE_33 = "Модуль 3 Д №3";
        private const string MANAGEMENT_KU_DISCRETE_34 = "Модуль 3 Д №4";
        private const string MANAGEMENT_KU_DISCRETE_35 = "Модуль 3 Д №5";
        private const string MANAGEMENT_KU_DISCRETE_36 = "Модуль 3 Д №6";
        private const string MANAGEMENT_KU_DISCRETE_37 = "Модуль 3 Д №7";
        private const string MANAGEMENT_KU_DISCRETE_38 = "Модуль 3 Д №8";




        private const string NO = "Нет";


        // Словарь где значение в устройстве соотносятся с режмам освещенгия
        private readonly Dictionary<int, LightingModeEnum> LightingValueNameDictionary = new Dictionary<int, LightingModeEnum>
        {
            {0, LightingModeEnum.UNDEFINED},
            { 1, LightingModeEnum.NIGHTLIGHTING},
            { 2, LightingModeEnum.ECONOMY_NIGHTLIGHTING},
            { 3, LightingModeEnum.ECONOMY_NIGHTLIGHTING},
            { 4, LightingModeEnum.ECONOMY_NIGHTLIGHTING},

            { 5, LightingModeEnum.ILLUMINATION},
            { 6, LightingModeEnum.ECONOMY_ILLUMINATION},
            { 7, LightingModeEnum.ECONOMY_ILLUMINATION},
            { 8, LightingModeEnum.ECONOMY_ILLUMINATION},


            {9, LightingModeEnum.BACKLIGHT},
            {10, LightingModeEnum.ECONOMY_BACKLIGHT},
            { 11, LightingModeEnum.ECONOMY_BACKLIGHT},
            { 12, LightingModeEnum.ECONOMY_BACKLIGHT},
            { 13, LightingModeEnum.HEATING}
            ,
        };


        // Маска реле неисправности канала
        private readonly ushort[] _maskRelayFaultCanal = new ushort[8]
        {
            0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080
        };

        public Picon2StarterFactory(IIoCContainer container)
        {
            _container = container;
        }




        #region Implementation of IStarterFactory

        public List<IStarter> CreateStarters(IDeviceContext deviceContext, byte[] configDataBytes)
        {
            List<IStarter> starters = new List<IStarter>();
            int chanelCount = 8;
            int starterCounter = 0;
            deviceContext.SchemeTable = new DefaultSchemeTable();
            int baseAddress = 0x3004;
           
            int startAddressKuOutput = 0x3020;
            int startAddressKuOutputInv = 0x3024;
            int offsetKuOutput = (startAddressKuOutput - baseAddress) * 2;
            int offsetInvKuOutput = (startAddressKuOutputInv - baseAddress) * 2;
            for (int channelNumber = 0; channelNumber < chanelCount; channelNumber++)
            {
                int inversionBytesCorrection = ((channelNumber % 2 == 0) || channelNumber == 0) ? 1 : -1;
                var dataValueKuOutput = configDataBytes[offsetKuOutput + channelNumber + inversionBytesCorrection];
                var dataValueInvKuOutput = configDataBytes[offsetInvKuOutput + channelNumber + inversionBytesCorrection];
                if(dataValueInvKuOutput==0&&dataValueKuOutput==0)continue;



                bool isChannelWithShedule = false;
                int offset = 0;
                if ((channelNumber == 0) || (channelNumber % 2 == 0))
                {
                    offset = 1 + channelNumber;
                }
                else
                {
                    offset = channelNumber - 1;
                }
                byte channelLightningMode = configDataBytes[2 * (int)((0x3028- baseAddress)  ) + offset];
                isChannelWithShedule = channelLightningMode != 0;

                if (isChannelWithShedule)
                {
                    starterCounter++;
                    IStarter starter = _container.Resolve<IStarter>();
                    starter.SetLogger(LogManager.GetLogger(deviceContext.DeviceName));
                    InitializeStarter(starter, deviceContext, channelNumber, configDataBytes,
                        channelLightningMode,baseAddress);
                    starters.Add(starter);
                }
            }

            return starters;
        }







        private void InitializeStarter(IStarter starter, IDeviceContext deviceContext, int channelNumber, byte[] data, byte lightingModeValue,int baseAddress)
        {
            channelNumber++;
            int starterNumber;

            if (uint.Parse(deviceContext.ChannelNumberOfStarter1) == channelNumber)
            {
                starterNumber = 1;
            }
            else if (uint.Parse(deviceContext.ChannelNumberOfStarter2) == channelNumber)
            {
                starterNumber = 2;
            }
            else if (uint.Parse(deviceContext.ChannelNumberOfStarter3) == channelNumber)
            {
                starterNumber = 3;
            }
            else
            {
                return;
            }

            starter.StarterNumber = starterNumber;
            starter.ChannelNumber = channelNumber;
            if (deviceContext.SchemeTable.GetStarterRow(starterNumber) != null)
            {
                return;
            }
            var row = new DefaultStarterRow();
            row.StarterId = starterNumber;

            if (starterNumber == 1)
            {
                row.StarterDescription = deviceContext.Starter1Description;

            }
            else if (starterNumber == 2)
            {
                row.StarterDescription = deviceContext.Starter2Description;
            }
            else if (starterNumber == 3)
            {
                row.StarterDescription = deviceContext.Starter3Description;
            }

            starter.StarterDescription = row.StarterDescription;

            deviceContext.SchemeTable.AddStarterRow(row);

            if (LightingValueNameDictionary.ContainsKey(lightingModeValue))
            {
                starter.StarterLightingMode = LightingValueNameDictionary[lightingModeValue];
            }
            else
            {
                starter.StarterLightingMode = LightingModeEnum.UNDEFINED;
            }
         ;

            ushort offsetAddress = 0x4;

            for (int modulIndex = 0; modulIndex < 8; modulIndex++)
            {
                int offsetModule = 0;
                if ((modulIndex == 0) || (modulIndex % 2 == 0))
                {
                    offsetModule = 1 + modulIndex;
                }
                else
                {
                    offsetModule = modulIndex - 1;
                }



                var faultValue = data[(2 * (0x3040-baseAddress) + (channelNumber - 1) * offsetAddress * 2) + offsetModule];
                for (int i = 0; i < _maskRelayFaultCanal.Length; i++)
                {
                    var mask = _maskRelayFaultCanal[i];
                    if ((faultValue & mask) > 0)
                    {
                        deviceContext.SchemeTable.AddResistorRow(new DefaultResistorRow
                        {
                            ResistorId = i + 8 * (i + 1) * modulIndex,
                            ResistorDiskret = i,
                            ResistorModul = 1 + modulIndex,
                            ParentStarterId = starterNumber,
                            ResistorDescription = String.Empty
                        });
                    }
                }
            }


        }


        #endregion
    }
}
