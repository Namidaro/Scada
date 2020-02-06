namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResistor
    {
        /// <summary>
        /// 
        /// </summary>
        int ModuleNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int ResistorNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsDefectState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsOnState { get; set; }

        IStarter ParentStarter { get; set; }


    }
}