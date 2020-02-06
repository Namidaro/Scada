namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExtraSettingsViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Описывает задержку в схеме в сукундах, которую будет выбирать пользователь
        /// </summary>
        int FullTimeout { get; set; }



        /// <summary>
        ///     Описывает задержку в списке в сукундах, которую будет выбирать пользователь
        /// </summary>
        int PartialTimeout { get; set; }


        /// <summary>
        ///     Описывает таймаут обменов, который будет выбирать пользователь
        /// </summary>
        int QueryTimeout { get; set; }

        /// <summary>
        ///     Минимальное значение для обновления в схеме
        /// </summary>
        int FullMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления в схеме
        /// </summary>
        int FullMaxValue { get; set; }

        /// <summary>
        ///     Минимальное значение для обновления в списке
        /// </summary>
        int PartialMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления в списке
        /// </summary>
        int PartialMaxValue { get; set; }



        /// <summary>
        ///     Минимальное значение для таймаута обменов
        /// </summary>
        int QueryMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для таймаута обменов
        /// </summary>
        int QueryMaxValue { get; set; }


        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        int NumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        int MinuteRepeatInterval { get; set; }
        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        int MaxNumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        int MaxMinuteRepeatInterval { get; set; }  
        
        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        int MinNumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        int MinMinuteRepeatInterval { get; set; }
        /// <summary>
        /// автоквитирование
        /// </summary>
        bool AcknowledgeEnabled { get; set; }

        /// <summary>
        ///     Результат завершения вызова вьюшки
        /// </summary>
        MessageBoxResult Result { get; set; }


    }
}