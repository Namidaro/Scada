using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NLog;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Devices.Runo3.Business.Interfaces;
using ULA.Interceptors;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Runo3.Business.Factories
{
    public class Runo3StarterFactory : IRuno3StarterFactory
    {
        private readonly IIoCContainer _container;

        private const string MANAGEMENT_KU_DISCRETE_1 = "Дискрет №1";
        private const string MANAGEMENT_KU_DISCRETE_2 = "Дискрет №2";
        private const string MANAGEMENT_KU_DISCRETE_3 = "Дискрет №3";
        private const string MANAGEMENT_KU_DISCRETE_4 = "Дискрет №4";
        private const string MANAGEMENT_KU_DISCRETE_5 = "Дискрет №5";
        private const string MANAGEMENT_KU_DISCRETE_6 = "Дискрет №6";
        private const string MANAGEMENT_KU_DISCRETE_7 = "Дискрет №7";
        private const string MANAGEMENT_KU_DISCRETE_8 = "Дискрет №8";
        private const string MANAGEMENT_KU_DISCRETE_9 = "Дискрет №9";
        private const string MANAGEMENT_KU_DISCRETE_10 = "Дискрет №10";
        private const string MANAGEMENT_KU_DISCRETE_11 = "Дискрет №11";
        private const string NO = "Нет";


        // Словарь где значение в устройстве соотносятся с режмам освещенгия
        private readonly Dictionary<int, LightingModeEnum> LightingValueNameDictionary = new Dictionary<int, LightingModeEnum>
        {
            {0, LightingModeEnum.UNDEFINED}, {1, LightingModeEnum.NIGHTLIGHTING}, {2, LightingModeEnum.BACKLIGHT},
            {3, LightingModeEnum.ILLUMINATION}, {4, LightingModeEnum.ENERGY_SAVING}, {5, LightingModeEnum.HEATING},
            {6, LightingModeEnum.ECONOMY_NIGHTLIGHTING}, {7, LightingModeEnum.ECONOMY_BACKLIGHT}, {8, LightingModeEnum.ECONOMY_ILLUMINATION}
        };


        // Маска реле неисправности канала
        private readonly ushort[] _maskRelayFaultCanal = new ushort[11]
        {
            0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080, 0x0100, 0x0200, 0x0400
        };


        // Словарь где имени дискрета управления соотносится его hex значение в устройстве
        private readonly Dictionary<string, ushort> _managementNameValueDictionary = new Dictionary<string, ushort>
        {
            {MANAGEMENT_KU_DISCRETE_1, 0x0001}, {MANAGEMENT_KU_DISCRETE_2, 0x0002}, {MANAGEMENT_KU_DISCRETE_3, 0x0003}, {MANAGEMENT_KU_DISCRETE_4, 0x0004},
            {MANAGEMENT_KU_DISCRETE_5, 0x0005}, {MANAGEMENT_KU_DISCRETE_6, 0x0006}, {MANAGEMENT_KU_DISCRETE_7, 0x0007}, {MANAGEMENT_KU_DISCRETE_8, 0x0008},
            {MANAGEMENT_KU_DISCRETE_9, 0x0009},  {MANAGEMENT_KU_DISCRETE_10, 0x000A}, {MANAGEMENT_KU_DISCRETE_11, 0x000B}
        };


        public Runo3StarterFactory(IIoCContainer container)
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
            for (int channelNumber = 0; channelNumber < chanelCount; channelNumber++)
            {
                var addressOfOutput = channelNumber * 12;
                var relayNumber = (ushort)configDataBytes[addressOfOutput + 3];
                if (relayNumber > 0 )
                {
                    starterCounter++;
                    if (starterCounter > 3)
                    {
                        break;
                    }
                    IStarter starter = _container.Resolve<IStarter>();
                    starter.SetLogger(LogManager.GetLogger(deviceContext.DeviceName));
                    InitializeStarter(starter, deviceContext, relayNumber, channelNumber, configDataBytes);
                    starters.Add(starter);
                }
            }

            return starters;
        }


        private void InitializeStarter(IStarter starter, IDeviceContext deviceContext, int relayNumber, int channelNumber, byte[] data)
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
            var addressOfLightingMode = (channelNumber-1) * 12;
            var lightingModeValueValue = (ushort)(data[addressOfLightingMode] * 256 + data[addressOfLightingMode + 1]);

            if (LightingValueNameDictionary.ContainsKey(lightingModeValueValue))
            {
                starter.StarterLightingMode = LightingValueNameDictionary[lightingModeValueValue];


            }
            else
            {
                starter.StarterLightingMode = LightingModeEnum.UNDEFINED;
            }

            // Маска реле неисправности канала

            var address = (channelNumber - 1) * 12;
            var faultValue = (ushort)(data[address + 4] * 256 + data[address + 5]);
            for (int i = 0; i < _maskRelayFaultCanal.Length; i++)
            {
                var mask = _maskRelayFaultCanal[i];
                if ((faultValue & mask) > 0)
                {
                    deviceContext.SchemeTable.AddResistorRow(new DefaultResistorRow
                    {
                        ResistorId = i + 1,
                        ResistorDiskret = i,
                        ResistorModul = 1,
                        ParentStarterId = starterNumber,
                        ResistorDescription = String.Empty
                    });
                }
            }

        }

        #endregion
    }
}
