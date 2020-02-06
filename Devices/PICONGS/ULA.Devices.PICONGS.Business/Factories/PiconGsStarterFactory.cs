using System;
using System.Collections.Generic;
using NLog;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors;
using ULA.Interceptors.IoC;

namespace ULA.Devices.PICONGS.Business.Factories
{
   public class PiconGsStarterFactory : IStarterFactory
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
        private const string MANAGEMENT_KU_DISCRETE_9 = "Модуль 1 Д №9";
        private const string MANAGEMENT_KU_DISCRETE_10 = "Модуль 1 Д №10";
        private const string MANAGEMENT_KU_DISCRETE_11 = "Модуль 1 Д №11";



        private const string MANAGEMENT_KU_DISCRETE_12 = "Модуль 2 Д №1";
        private const string MANAGEMENT_KU_DISCRETE_13 = "Модуль 2 Д №2";
        private const string MANAGEMENT_KU_DISCRETE_14 = "Модуль 2 Д №3";
        private const string MANAGEMENT_KU_DISCRETE_15 = "Модуль 2 Д №4";
        private const string MANAGEMENT_KU_DISCRETE_16 = "Модуль 2 Д №5";
        private const string MANAGEMENT_KU_DISCRETE_17 = "Модуль 2 Д №6";
        private const string MANAGEMENT_KU_DISCRETE_18 = "Модуль 2 Д №7";
        private const string MANAGEMENT_KU_DISCRETE_19 = "Модуль 2 Д №8";
        private const string MANAGEMENT_KU_DISCRETE_20 = "Модуль 2 Д №9";
        private const string MANAGEMENT_KU_DISCRETE_21 = "Модуль 2 Д №10";
        private const string MANAGEMENT_KU_DISCRETE_22 = "Модуль 2 Д №11";


        private const string MANAGEMENT_KU_DISCRETE_23 = "Модуль 3 Д №1";
        private const string MANAGEMENT_KU_DISCRETE_24 = "Модуль 3 Д №2";
        private const string MANAGEMENT_KU_DISCRETE_25 = "Модуль 3 Д №3";
        private const string MANAGEMENT_KU_DISCRETE_26 = "Модуль 3 Д №4";
        private const string MANAGEMENT_KU_DISCRETE_27 = "Модуль 3 Д №5";
        private const string MANAGEMENT_KU_DISCRETE_28 = "Модуль 3 Д №6";
        private const string MANAGEMENT_KU_DISCRETE_29 = "Модуль 3 Д №7";
        private const string MANAGEMENT_KU_DISCRETE_30 = "Модуль 3 Д №8";
        private const string MANAGEMENT_KU_DISCRETE_31 = "Модуль 3 Д №9";
        private const string MANAGEMENT_KU_DISCRETE_32 = "Модуль 3 Д №10";
        private const string MANAGEMENT_KU_DISCRETE_33 = "Модуль 3 Д №11";




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


        private readonly Dictionary<string, ushort> _managementNameValueDictionary = new Dictionary<string, ushort>
        {
            {MANAGEMENT_KU_DISCRETE_1, 0x0001},
            {MANAGEMENT_KU_DISCRETE_2, 0x0002},
            {MANAGEMENT_KU_DISCRETE_3, 0x0003},
            {MANAGEMENT_KU_DISCRETE_4, 0x0004},
            {MANAGEMENT_KU_DISCRETE_5, 0x0005},
            {MANAGEMENT_KU_DISCRETE_6, 0x0006},
            {MANAGEMENT_KU_DISCRETE_7, 0x0007},
            {MANAGEMENT_KU_DISCRETE_8, 0x0008},
            {MANAGEMENT_KU_DISCRETE_9, 0x0009},
            {MANAGEMENT_KU_DISCRETE_10, 0x000A},
            {MANAGEMENT_KU_DISCRETE_11, 0x000B},
            {NO, 0x0000},


            {MANAGEMENT_KU_DISCRETE_12, 0x000C},
            {MANAGEMENT_KU_DISCRETE_13, 0x000D},
            {MANAGEMENT_KU_DISCRETE_14, 0x000E},
            {MANAGEMENT_KU_DISCRETE_15, 0x000F},
            {MANAGEMENT_KU_DISCRETE_16, 0x0010},
            {MANAGEMENT_KU_DISCRETE_17, 0x0011},
            {MANAGEMENT_KU_DISCRETE_18, 0x0012},
            {MANAGEMENT_KU_DISCRETE_19, 0x0013},
            {MANAGEMENT_KU_DISCRETE_20, 0x0014},
            {MANAGEMENT_KU_DISCRETE_21, 0x0015},
            {MANAGEMENT_KU_DISCRETE_22, 0x0016},



            {MANAGEMENT_KU_DISCRETE_23, 0x0017},
            {MANAGEMENT_KU_DISCRETE_24, 0x0018},
            {MANAGEMENT_KU_DISCRETE_25, 0x0019},
            {MANAGEMENT_KU_DISCRETE_26, 0x001A},
            {MANAGEMENT_KU_DISCRETE_27, 0x001B},
            {MANAGEMENT_KU_DISCRETE_28, 0x001C},
            {MANAGEMENT_KU_DISCRETE_29, 0x001D},
            {MANAGEMENT_KU_DISCRETE_30, 0x001E},
            {MANAGEMENT_KU_DISCRETE_31, 0x001F},
            {MANAGEMENT_KU_DISCRETE_32, 0x0020},
            {MANAGEMENT_KU_DISCRETE_33, 0x0021},
        };


        public PiconGsStarterFactory(IIoCContainer container)
        {
            _container = container;
        }




        #region Implementation of IStarterFactory

        public List<IStarter> CreateStarters(IDeviceContext deviceContext, byte[] configDataBytes)
        {
            List<IStarter> starters=new List<IStarter>();
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
                    InitializeStarter(starter,deviceContext,relayNumber, channelNumber, configDataBytes);
                    starters.Add(starter);
                }
            }

            return starters;
        }







        private void InitializeStarter(IStarter starter,IDeviceContext deviceContext,int relayNumber, int channelNumber, byte[] data)
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
            var addressOfLightingMode = (channelNumber - 1) * 12;
            var lightingModeValueValue = (ushort)(data[addressOfLightingMode] * 256 + data[addressOfLightingMode + 1]);

            if (LightingValueNameDictionary.ContainsKey(lightingModeValueValue))
            {
                starter.StarterLightingMode = LightingValueNameDictionary[lightingModeValueValue];

            
            }
            else
            {
                starter.StarterLightingMode= LightingModeEnum.UNDEFINED;
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


            var faultValue2 = (ushort)(data[address + 6] * 256 + data[address + 7]);
            for (int i = 0; i < _maskRelayFaultCanal.Length; i++)
            {
                var mask = _maskRelayFaultCanal[i];
                if ((faultValue2 & mask) > 0)
                {
                    deviceContext.SchemeTable.AddResistorRow(new DefaultResistorRow
                    {
                        ResistorId = i +11+ 1,
                        ResistorDiskret = i,
                        ResistorModul = 2,
                        ParentStarterId = starterNumber,
                        ResistorDescription = String.Empty
                    });
                }
            }


            var faultValue3 = (ushort)(data[address + 8] * 256 + data[address + 9]);
            for (int i = 0; i < _maskRelayFaultCanal.Length; i++)
            {
                var mask = _maskRelayFaultCanal[i];
                if ((faultValue3 & mask) > 0)
                {
                    deviceContext.SchemeTable.AddResistorRow(new DefaultResistorRow
                    {
                        ResistorId = i +11*2+ 1,
                        ResistorDiskret = i,
                        ResistorModul =3,
                        ParentStarterId = starterNumber,
                        ResistorDescription = String.Empty
                    });
                }
            }


            var faultValue4 = (ushort)(data[address + 10] * 256 + data[address + 11]);
            for (int i = 0; i < _maskRelayFaultCanal.Length; i++)
            {
                var mask = _maskRelayFaultCanal[i];
                if ((faultValue4 & mask) > 0)
                {
                    deviceContext.SchemeTable.AddResistorRow(new DefaultResistorRow
                    {
                        ResistorId = i+11*3 + 1,
                        ResistorDiskret = i,
                        ResistorModul = 4,
                        ParentStarterId = starterNumber,
                        ResistorDescription = String.Empty
                    });
                }
            }


        }


        #endregion
    }
}
