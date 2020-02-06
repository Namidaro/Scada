using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULA.AddinsHost.Business.Strings
{
    /// <summary>
    /// строки с названиями адресов
    /// </summary>
    public static class AddressesStrings
    {
        ///// <summary>
        ///// строка адреса неисправности двери ШНО
        ///// </summary>
        //public const string DOOR_SHNO_FAIL = "DOOR_SHNO_FAIL";

        ///// <summary>
        ///// строка адреса неисправности НО
        ///// </summary>
        //public const string NIGHT_LIGHT_FAIL = "NIGHT_LIGHT_FAIL";

        ///// <summary>
        ///// строка адреса неисправности ВО
        ///// </summary>
        //public const string EVENING_LIGHT_FAIL = "EVENING_LIGHT_FAIL";

        ///// <summary>
        ///// строка адреса неисправности ключа ЦУ
        ///// </summary>
        //public const string KEY_MAIN_MANAGEMENT_FAIL = "KEY_MAIN_MANAGEMENT_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф1 ПР1
        ///// </summary>
        //public const string FIDER_1_FUSE1_FAIL = "FIDER_1_FUSE1_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф1 ПР2
        ///// </summary>
        //public const string FIDER_1_FUSE2_FAIL = "FIDER_1_FUSE2_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф1 ПР3
        ///// </summary>
        //public const string FIDER_1_FUSE3_FAIL = "FIDER_1_FUSE3_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф2 ПР1
        ///// </summary>
        //public const string FIDER_2_FUSE1_FAIL = "FIDER_2_FUSE1_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф2 ПР2
        ///// </summary>
        //public const string FIDER_2_FUSE2_FAIL = "FIDER_2_FUSE2_FAIL";

        ///// <summary>
        ///// строка адреса неисправности Ф2 ПР3
        ///// </summary>
        //public const string FIDER_2_FUSE3_FAIL = "FIDER_2_FUSE3_FAIL";




        ///// <summary>
        ///// строка адреса состояния двери ШНО
        ///// </summary>
        //public const string DOOR_SHNO_STATE = "DOOR_SHNO_STATE";

        ///// <summary>
        ///// строка адреса состояния НО
        ///// </summary>
        //public const string NIGHT_LIGHT_STATE = "NIGHT_LIGHT_STATE";

        ///// <summary>
        ///// строка адреса состояния ВО
        ///// </summary>
        //public const string EVENING_LIGHT_STATE = "EVENING_LIGHT_STATE";

        ///// <summary>
        ///// строка адреса состояния ключа ЦУ
        ///// </summary>
        //public const string KEY_MAIN_MANAGEMENT_STATE = "KEY_MAIN_MANAGEMENT_STATE";


        ///// <summary>
        ///// строка адреса состояния Ф1 ПР1
        ///// </summary>
        //public const string FIDER_1_FUSE1_STATE = "FIDER_1_FUSE1_STATE";

        ///// <summary>
        ///// строка адреса состояния Ф1 ПР2
        ///// </summary>
        //public const string FIDER_1_FUSE2_STATE = "FIDER_1_FUSE2_STATE";

        ///// <summary>
        ///// строка адреса состояния Ф1 ПР3
        ///// </summary>
        //public const string FIDER_1_FUSE3_STATE = "FIDER_1_FUSE3_STATE";

        ///// <summary>
        ///// строка адреса состояния Ф2 ПР1
        ///// </summary>
        //public const string FIDER_2_FUSE1_STATE = "FIDER_2_FUSE1_STATE";

        ///// <summary>
        ///// строка адреса состояния Ф2 ПР2
        ///// </summary>
        //public const string FIDER_2_FUSE2_STATE = "FIDER_2_FUSE2_STATE";

        ///// <summary>
        ///// строка адреса состояния Ф2 ПР3
        ///// </summary>
        //public const string FIDER_2_FUSE3_STATE = "FIDER_2_FUSE3_STATE";

        /// <summary>
        /// строка паттерна неисправности фидера с предохранителем
        /// </summary>
        public const string MODUL_DISKRET_FAIL_PATTERN = @"MODUL{0}_DISKRET{1}_FAIL";
        /// <summary>
        /// строка паттерна неисправности фидера с предохранителем
        /// </summary>
        public const string MODUL_DISKRET_STATE_PATTERN = @"MODUL{0}_DISKRET{1}_STATE";


        /// <summary>
        /// строка паттерна фидера 
        /// </summary>
        public const string FIDER_PATTERN = @"FIDER_{0}";

        /// <summary>
        /// строка неисправность 
        /// </summary>
        public const string FAIL_STR ="FAIL";

        /// <summary>
        /// строка состояние 
        /// </summary>
        public const string STATE_STR = "STATE";

        /// <summary>
        /// строка модуль
        /// </summary>
        public const string MODUL_STR = "MODUL";

        /// <summary>
        /// строка дискрет
        /// </summary>
        public const string DISKRET_STR = "DISKRET";

        /// <summary>
        /// строка раздерителя
        /// </summary>
        public const string SPLITTER_STR = "_";
        /// <summary>
        /// строка определяющая нормально включенное состояние сигнала
        /// </summary>
        public const string IS_INDICATOR_STR = "♪♪";
    }
}

