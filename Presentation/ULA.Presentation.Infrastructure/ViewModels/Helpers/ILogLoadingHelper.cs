using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ULA.Presentation.Infrastructure.ViewModels.Helpers
{
    public interface ILogLoadingHelper
    {
       Task<IEnumerable<ILogMessageViewModel>> LoadLogMessageViewModels();

    }
}