using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomFider:ITagNamedObject
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsEnabledResistor1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsEnabledResistor2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsEnabledResistor3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ModuleResistor1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int ModuleResistor2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int ModuleResistor3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int NumberResistor1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int NumberResistor2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int NumberResistor3 { get; set; }


    }
}