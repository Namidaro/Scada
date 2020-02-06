using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ULA.Presentation.Infrastructure.ViewModels.Log
{
    public interface IApplicationLogViewModel
    {
        ICollectionView LogMessageViewModels { get; }

        string DescriptionFilter { get; set; }

        void ShowLog();


        bool IsLoadingInProcess { get; set; }

        ObservableCollection<string> AvailableOwners { get; set; }
        string SelectedOwner { get; set; }


        ObservableCollection<string> AvailableMessageTypes { get; set; }
        string SelectedMessageType { get; set; }

        
    }
}