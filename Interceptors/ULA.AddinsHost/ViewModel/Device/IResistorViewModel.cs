namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResistorViewModel
    {
        bool? IsDefectState { get;  }
        bool? IsStarterManagementFuseStateOn { get; }
        bool? IsOnState { get;  }
        int ParentStarterId { get; }
        string ShortParentStarterDescription { get;  }
        object Model { get; set; }
        int ModuleNumber { get;  }
        int ResistorNumber { get; }

        void SetLostConnection();

        void UpdateStates();
        bool IsEnabled { get; set; }
    }
}