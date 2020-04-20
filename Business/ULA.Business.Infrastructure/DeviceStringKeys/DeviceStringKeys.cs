using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULA.Business.Infrastructure.DeviceStringKeys
{

    /// <summary>
    /// класс для хранения ключей 
    /// </summary>
    public static class DeviceStringKeys
    {


        //public static class DeviceStarterStatesTagKeys
        //{
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_NIGHTLIGHING_STARTER = "State.Switching.NightLighting";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_NIGHTLIGHING_STARTER = "State.ManualMode.NightLighting";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_NIGHTLIGHING_STARTER = "State.Repair.NightLighting";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_EVENINGLIGHING_STARTER = "State.Switching.EveningLighting";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_EVENINGLIGHING_STARTER = "State.ManualMode.EveningLighting";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_EVENINGLIGHING_STARTER = "State.Repair.EveningLighting";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_ILLUMINATION_STARTER = "State.Switching.Illumination";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_ILLUMINATION_STARTER = "State.ManualMode.Illumination";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_ILLUMINATION_STARTER = "State.Repair.Illumination";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_HEATING_STARTER = "State.Switching.Heating";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_HEATING_STARTER = "State.ManualMode.Heating";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_HEATING_STARTER = "State.Repair.Heating";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_BACKLIGHT_STARTER = "State.Switching.Backlight";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_BACKLIGHT_STARTER = "State.ManualMode.Backlight";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_BACKLIGHT_STARTER = "State.Repair.Backlight";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_ENERGYSAVING_STARTER = "State.Switching.EnegrySaving";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_ENERGYSAVING_STARTER = "State.ManualMode.EnegrySaving";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_ENERGYSAVING_STARTER = "State.Repair.EnegrySaving";






        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_NIGHTLIGHING2_STARTER = "State.Switching.NightLighting2";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_NIGHTLIGHING2_STARTER = "State.ManualMode.NightLighting2";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_NIGHTLIGHING2_STARTER = "State.Repair.NightLighting2";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_SWITCHING_EVENINGLIGHING2_STARTER = "State.Switching.EveningLighting2";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string STATE_MANUALMODE_EVENINGLIGHING2_STARTER = "State.ManualMode.EveningLighting2";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_REPAIR_EVENINGLIGHING2_STARTER = "State.Repair.EveningLighting2";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_NIGHTLIGHTING_STARTER = "State.ManegementFuse.NightLighting";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_EVENINGLIGHING_STARTER = "State.ManegementFuse.EveningLighting";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_ILLUMINATION_STARTER = "State.ManegementFuse.Illumination";
        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_HETAING_STARTER = "State.ManegementFuse.Heating";
        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_BACKLIGHT_STARTER = "State.ManegementFuse.Backlight";
        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_ENEGRYSAVING_STARTER = "State.ManegementFuse.EnergySaving";
        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_NIGHTLIGHTING2_STARTER = "State.ManegementFuse.NightLighting2";
        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string STATE_MANAGEMENT_FUSE_EVENINGLIGHING2_STARTER = "State.ManegementFuse.EveningLighting2";

        //}





        ///// <summary>
        ///// тэги для команд
        ///// </summary>

        //public static class DeviceCommandsTagKeys
        //{

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_NIGHTLIGHING_STARTER = "Command.Switching.NightLighting";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_NIGHTLIGHING_STARTER = "Command.ManualMode.NightLighting";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_NIGHTLIGHING_STARTER = "Command.Repair.NightLighting";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_EVENINGLIGHING_STARTER = "Command.Switching.EveningLighting";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_EVENINGLIGHING_STARTER = "Command.ManualMode.EveningLighting";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_EVENINGLIGHING_STARTER = "Command.Repair.EveningLighting";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_ILLUMINATION_STARTER = "Command.Switching.Illumination";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_ILLUMINATION_STARTER = "Command.ManualMode.Illumination";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_ILLUMINATION_STARTER = "Command.Repair.Illumination";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_HEATING_STARTER = "Command.Switching.Heating";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_HEATING_STARTER = "Command.ManualMode.Heating";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_HEATING_STARTER = "Command.Repair.Heating";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_BACKLIGHT_STARTER = "Command.Switching.Backlight";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_BACKLIGHT_STARTER = "Command.ManualMode.Backlight";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_BACKLIGHT_STARTER = "Command.Repair.Backlight";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_ENERGYSAVING_STARTER = "Command.Switching.EnegrySaving";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_ENERGYSAVING_STARTER = "Command.ManualMode.EnegrySaving";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_ENERGYSAVING_STARTER = "Command.Repair.EnegrySaving";






        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_NIGHTLIGHING2_STARTER = "Command.Switching.NightLighting2";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_NIGHTLIGHING2_STARTER = "Command.ManualMode.NightLighting2";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_NIGHTLIGHING2_STARTER = "Command.Repair.NightLighting2";


        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_SWITCHING_EVENINGLIGHING2_STARTER = "Command.Switching.EveningLighting2";

        //    /// <summary>
        //    /// команда ручной режим пускателя
        //    /// </summary>
        //    public const string COMMAND_MANUALMODE_EVENINGLIGHING2_STARTER = "Command.ManualMode.EveningLighting2";

        //    /// <summary>
        //    /// команда включения пускателя
        //    /// </summary>
        //    public const string COMMAND_REPAIR_EVENINGLIGHING2_STARTER = "Command.Repair.EveningLighting2";



        //}




        /// <summary>
        /// нет подключения
        /// </summary>
        public const string NO_CONNECTION_STRING = "NoConnection";

        /// <summary>
        /// тэги для даты-времени
        /// </summary>

        public static class DeviceCommandsTagKeys
        {
            /// <summary>
            /// команда включения первого пускателя
            /// </summary>
            public const string COMMAND_SWITCHING_STARTER = "Command.Switching.Starter";

            /// <summary>
            /// команда ручной режим первого пускателя
            /// </summary>
            public const string COMMAND_MANUALMODE_STARTER = "Command.ManualMode.Starter";

            /// <summary>
            /// команда включения первого пускателя
            /// </summary>
            public const string COMMAND_REPAIR_STARTER = "Command.Repair.Starter";
         

        }

        /// <summary>
        /// тэги для счетчиков
        /// </summary>

        public static class DeviceAnalogMetersTagKeys
        {
            public const string METER_DATE_TIME = "MeterDateTime";


            /// <summary>
            /// 
            /// </summary>
            public const string ANALOG_METER_TIME = "AnaloMeter.Time";
            /// <summary>
            /// 
            /// </summary>
            public const string ANALOG_METER_DATE = "AnalogMeter.Date";


            /// <summary>
            /// 
            /// </summary>
            public const string NO = "No";

            /// <summary>
            /// 
            /// </summary>
            public const string ANALOG_TIMER = "AnalogTimer";
            /// <summary>
            /// 
            /// </summary>
            public const string ANALOG_ID = "Analog";
            /// <summary>
            /// тип счетчика
            /// </summary>
            public const string ENERGOMERA_METER_TYPE = "Energomera";


            /// <summary>
            /// тип счетчика
            /// </summary>
            public const string MSA_METER_TYPE = "MSA.Meter";

            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_A_MSA961 = "Voltage.A.Msa961";
            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_B_MSA961 = "Voltage.B.Msa961";
            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_C_MSA961 = "Voltage.C.Msa961";


            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_A_MSA961 = "Current.A.Msa961";
            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_B_MSA961 = "Current.B.Msa961";
            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_С_MSA961 = "Current.C.Msa961";

            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_A = "Voltage.A";
            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_B = "Voltage.B";
            /// <summary>
            /// напряжение
            /// </summary>
            public const string VOLTAGE_C = "Voltage.C";


            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_A = "Current.A";
            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_B = "Current.B";
            /// <summary>
            /// ток
            /// </summary>
            public const string CURRENT_С = "Current.C";

            /// <summary>
            /// мощность
            /// </summary>
            public const string POWER_A = "Power.A";
            /// <summary>
            /// мощность
            /// </summary>
            public const string POWER_B = "Power.B";
            /// <summary>
            /// мощность
            /// </summary>
            public const string POWER_C = "Power.C";



            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY = "StoredEnergy";
            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY_1 = "StoredEnergy.1";
            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY_FOR_MOUNTH = "StoredEnergy.ForMounth";
            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY_FOR_MOUNTH_1 = "StoredEnergy.ForMounth.1";
            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY_FOR_DAY = "StoredEnergy.ForDay";
            /// <summary>
            /// накопленнная энергия
            /// </summary>
            public const string STORED_ENERGY_FOR_DAY_1 = "StoredEnergy.ForDay.1";




        }


        /// <summary>
        /// тэги для даты-времени
        /// </summary>

        public static class DeviceDateTimeTagKeys
        {
            /// <summary>
            /// год
            /// </summary>
            public const string DATETIME_YEAR = "DateTime.Year";
            /// <summary>
            /// месяц
            /// </summary>
            public const string DATETIME_MONTH = "DateTime.Month";
            /// <summary>
            /// день
            /// </summary>
            public const string DATETIME_DAY = "DateTime.Day";
            /// <summary>
            /// день в неделе
            /// </summary>
            public const string DATETIME_DAYINWEEK = "DateTime.DayInWeek";
            /// <summary>
            /// час
            /// </summary>
            public const string DATETIME_HOUR = "DateTime.Hour";
            /// <summary>
            /// минута
            /// </summary>
            public const string DATETIME_MINUTE = "DateTime.Minute";
            /// <summary>
            /// секунда
            /// </summary>
            public const string DATETIME_SECOND = "DateTime.Second";
            /// <summary>
            /// миллисекунда
            /// </summary>
            public const string DATETIME_MILLISECOND = "DateTime.Millisecond";

        }



        /// <summary>
        /// парсеры из байт в состояние устройства
        /// </summary>

        public static class DeviceBytesParsers
        {
            public static string ANALOG_DATETIME_PARSER="AnalogDateTimeParser";

            /// <summary>
            /// парсер для отображения в режиме списка
            /// </summary>
            public const string ANALOGS_PARSER = "AnalogsParser";


            /// <summary>
            /// парсер для отображения в режиме списка
            /// </summary>
            public const string PARTIAL_MODE_PARSER = "PartialModeParser";

            /// <summary>
            /// парсер даты и времени
            /// </summary>
            public const string DATETIME_PARSER = "DateTimeParser";

            /// <summary>
            /// парсер уровня сигнала
            /// </summary>
            public const string SIGNAL_LEVEL_PARSER = "SignalLevelParser";

            /// <summary>
            /// парсер подписи устройства
            /// </summary>
            public const string DEVICE_SIGNATURE_PARSER = "DeviceSignatureParser";

        }


        /// <summary>
        ///  ключи таблиц
        /// </summary>
        public static class DeviceTableTagKeys
        {





            /// <summary>
            /// 
            /// </summary>
            public const string SHEDULE1_DATA_ID_NAME = "DataIdShedule1";
            /// <summary>
            /// 
            /// </summary>
            public const string SHEDULE2_DATA_ID_NAME = "DataIdShedule2";
            /// <summary>
            /// 
            /// </summary>
            public const string SHEDULE3_DATA_ID_NAME = "DataIdShedule3";



            /// <summary>
            /// 
            /// </summary>
            public const string ACKNOWLEDGE_VALUE = "FailAcknowledge";

            /// <summary>
            /// 
            /// </summary>
            public const string COMMAND_MANAGENT_ID_NAME = "CommandManagent";

            /// <summary>
            /// 
            /// </summary>
            public const string DATETIME_ID_NAME = "DateTime";
            /// <summary>
            /// 
            /// </summary>
            public const string SIGNAL_LEVEL_DATA_ID_NAME = "SignalLevelId";

            /// <summary>
            /// 
            /// </summary>
            public const string SIGNAL_LEVEL = "SignalLevel";

            /// <summary>
            /// 
            /// </summary>
            public const string DEVICE_SIGNATURE_DATA_ID_NAME = "DeviceSignatureId";

            /// <summary>
            /// 
            /// </summary>
            public const string DEVICE_SIGNATURE = "DeviceSignature";



            /// <summary>
            /// состояние пускателя
            /// </summary>
            public const string STARTER_STATE_PATTERN = "State.Starter";




            /// <summary>
            /// контроль управления первого пускателя
            /// </summary>
            public const string STARTER_MANAGEMENT_CONTROL = "ManagementControl.Starter";


            /// <summary>
            /// авторежим пускателя
            /// </summary>
            public const string STARTER_IS_MANUAL_MODE_PATTERN = "IsManualMode.Starter";



            /// <summary>
            /// режим ремонта  пускателя
            /// </summary>
            public const string STARTER_IS_REPAIR = "IsRepair.Starter";



            /// <summary>
            /// неисправность цепей управления
            /// </summary>
            public const string MANAGEMENT_CHAINS_DEFECT = "Defect.ManagmentChains";
            /// <summary>
            /// неисправность предохранителей
            /// </summary>
            public const string FUSES_DEFECT = "Defect.Fuses";
            /// <summary>
            /// неисправность управления
            /// </summary>
            public const string MANAGEMENT_DEFECT = "Defect.Managment";


            /// <summary>
            /// неисправность охраны
            /// </summary>
            public const string PROTECTION_DEFECT = "Defect.Protection";
            /// <summary>
            /// неисправность питания
            /// </summary>
            public const string POWER_DEFECT = "Defect.NoPower";

           

            /// <summary>
            /// диагностика неисправности
            /// </summary>
            public const string DEFECT_DIAGNOSTIC = "Defect.Diagnostics";

            /// <summary>
            /// неисправность ПЗУ
            /// </summary>
            public const string DEFECT_PROGRAM_CODE = "Defect.ProgramCode";
            /// <summary>
            /// неисправность часов
            /// </summary>
            public const string DEFECT_CLOCK = "Defect.Clock";
            /// <summary>
            /// неиспрвность ППЗУ
            /// </summary>
            public const string DEFECT_PPZU = "Defect.PPZU";
            /// <summary>
            /// неисправность питания
            /// </summary>
            public const string DEFECT_POWER = "Defect.Power";
            /// <summary>
            /// неисправность связи с подчиненным
            /// </summary>
            public const string DEFECT_SLAVE_CONNECTION = "Defect.SlaveConnection";
            /// <summary>
            /// неисправность запросов к подчиненному
            /// </summary>
            public const string DEFECT_SLAVE_REQUEST = "Defect.SlaveRequest";
            /// <summary>
            /// неисправность логики
            /// </summary>
            public const string DEFECT_LOGIC = "Defect.Logic";
            /// <summary>
            /// неисправность модулей
            /// </summary>
            public const string MODULE_DEFECT = "Defect.Module";
            /// <summary>
            /// неисправность запросов к модулям
            /// </summary>
            public const string MODULE_REQUEST_DEFECT = "Defect.ModuleRequest";

            /// <summary>
            /// кунал управления 1
            /// </summary>
            public const string CONTROL_CHANNEL_1_CONTROL_MODE = "ControlChannel.1ControlMode";

        }
    }
}
