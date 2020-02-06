using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;

namespace ULA.Devices.PICONGS.Presentation.ViewModels.UserControl
{
    /// <summary>
    ///     Представляет вью-модель для контрола с чекбоксами(Будет использоваться в конфигурации
    ///     устройства)
    /// </summary>
    public class PICONGSConfigCheckBoxControlViewModel : ViewModelBase, IConfigCheckBoxControlViewModel
    {
        #region [Private fields]

        private ObservableCollection<bool> _checkBoxResult;
        private int _checkBoxCount;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Конструктор
        /// </summary>
        public PICONGSConfigCheckBoxControlViewModel()
        {
            this._checkBoxCount = 44;
            var list = new List<bool>();
            for (int i = 0; i < this._checkBoxCount; i++)
            {
                list.Add(false);
            }
            this.CheckBoxResult = new ObservableCollection<bool>(list);
            this.CheckBoxResult.CollectionChanged += CheckBoxResultOnCollectionChanged;
        }

        private void CheckBoxResultOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            OnPropertyChanged(nameof(CheckBoxResult));
            OnPropertyChanged(nameof(IsAtLeastOneSelected));
        }

        #endregion [Ctor's]

        #region [Properties]

        /// <summary>
        ///     Свойство представляющее битовую маску чекбоксов
        /// </summary>
        public ObservableCollection<bool> CheckBoxResult
        {
            get { return this._checkBoxResult; }
            set
            {
                if(this._checkBoxResult !=null && this._checkBoxResult.Equals(value)) return;
                this._checkBoxResult = value;
                OnPropertyChanged(nameof(CheckBoxResult));
                OnPropertyChanged(nameof(IsAtLeastOneSelected));

            }
        }

        /// <summary>
        ///     Индексатор для коллекция представляющей результаты чекбоксов
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                if (index < 0 || index > CheckBoxResult.Count)
                {
                    throw new IndexOutOfRangeException("Index must be from 0 to checkBoxCollection length");
                }

                return this.CheckBoxResult[index];
            }
            set
            {
                if (this.CheckBoxResult[index].Equals(value)) return;
                this.CheckBoxResult[index] = value;
                OnPropertyChanged(nameof(CheckBoxResult));
                OnPropertyChanged(nameof(IsAtLeastOneSelected));
            }
        }

        #endregion [Properties]

        #region [IConfigCheckBoxControlViewModel]

        /// <summary>
        ///     Возвращает будевый массив - снимок результатов чекбоксов в реальном времени
        /// </summary>
        /// <returns></returns>
        public bool[] GetCheckBoxBytes()
        {
            if (this._checkBoxResult == null)
                return null;
            return this._checkBoxResult.ToArray();
        }

        /// <summary>
        /// показывает, выдерен ли хоть один чекбокс
        /// </summary>
        public bool IsAtLeastOneSelected => CheckBoxResult.Any(c => c);

        #endregion [IConfigCheckBoxControlViewModel]

        #region [Override]

        /// <summary>
        ///     Освобождает ресурсы
        /// </summary>
        protected override void OnDisposing()
        {
            this.CheckBoxResult.CollectionChanged -= CheckBoxResultOnCollectionChanged;
            base.OnDisposing();
        } 

        #endregion
    }
}
