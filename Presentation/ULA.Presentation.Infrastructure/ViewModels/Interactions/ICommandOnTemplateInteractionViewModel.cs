using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    public interface ICommandOnTemplateInteractionViewModel : IInteractionViewModel
    {
        string Title { get; set; }

        ICommand EditTemplateCommand { get; }
        ICommand SaveTemplateCommand { get; }
        ICommand SaveAsTemplateCommand { get; }
        ICommand DeleteTemplateCommand { get; }
        ICommand ExecuteTemplateCommand { get; }
        ICommand ExitWindowCommand { get; }



    }
}
