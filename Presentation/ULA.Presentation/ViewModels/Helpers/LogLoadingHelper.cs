using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ULA.Interceptors.Application;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure.Factories;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Helpers;

namespace ULA.Presentation.ViewModels.Helpers
{
   public class LogLoadingHelper: ILogLoadingHelper
    {
        private readonly IApplicationSettingsService _settingsService;
        private readonly ILogMessageViewModelFactory _logMessageViewModelFactory;

        public LogLoadingHelper(IApplicationSettingsService settingsService,ILogMessageViewModelFactory logMessageViewModelFactory)
        {
            _settingsService = settingsService;
            _logMessageViewModelFactory = logMessageViewModelFactory;
        }


        #region Implementation of ILogLoadingHelper

        public async Task<IEnumerable<ILogMessageViewModel>> LoadLogMessageViewModels()
        {
            ConcurrentBag<ILogMessageViewModel> logMessageViewModels=new ConcurrentBag<ILogMessageViewModel>();
           
            await Task.Run((() =>
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
                var allFiles = directoryInfo.GetFiles();

                string nlog_file_pattern = @"nlog_[0-3][0-9]_[0-9]{4}_app" +
                                           _settingsService.ApplicationNumber.ToString() + ".txt";
                const string DATE_TIME_PATTERN = @"\d{2}\.\d{2}\.\d{4}\s\d{1,2}:\d{2}:\d{2}";
                foreach (var file in allFiles)
                {
                    if (Regex.IsMatch(file.Name, nlog_file_pattern))
                    {
                        var allLogs = File.ReadAllLines(file.Name,
                            Encoding.Unicode).ToList();
                        Parallel.ForEach(allLogs, ((log) =>
                        {
                            if (Regex.IsMatch(log, DATE_TIME_PATTERN))
                            {
                                var dateTimeString = Regex.Match(log, DATE_TIME_PATTERN).Value;
                                var dateTime = Convert.ToDateTime(dateTimeString, new CultureInfo("de-DE"));

                                var matchCollection = Regex.Matches(log, "\\[([^\\[\\]]+)\\]");
                                if (matchCollection.Count == 3)
                                {
                                    string messageTypeString = matchCollection[2].Value.Replace("[", String.Empty)
                                        .Replace("]", String.Empty);
                                    string owner = matchCollection[0].Value.Replace("[", String.Empty)
                                        .Replace("]", String.Empty);

                                    string message = log.Remove(0, log.LastIndexOf("]") + 1);

                                    ILogMessageViewModel logMessageViewModel =
                                        _logMessageViewModelFactory.CreateLogMessageViewModel(messageTypeString,
                                            message,
                                            owner,
                                            dateTime);
                               
                                    logMessageViewModels.Add(logMessageViewModel);
                                }
                            }
                        }));
                    }
                }
            }));
            return new ObservableCollection<ILogMessageViewModel>(logMessageViewModels);
        }

        #endregion
    }
}
