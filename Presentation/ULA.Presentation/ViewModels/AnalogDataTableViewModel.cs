using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.Enums;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Views;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    /// вью-модель для показания счетчиков
    /// </summary>
    public class AnalogDataTableViewModel : ViewModelBase
    {
        private bool _isMSA961Checked;
        private int _transKoef;

        /// <summary>
        /// вью-модель для показания счетчиков
        /// </summary>
        /// <param name="analogMeterType"></param>
        /// <param name="deviceViewModel"></param>
        public AnalogDataTableViewModel(string analogMeterType, IRuntimeDeviceViewModel deviceViewModel)
        {

         
            if (deviceViewModel.GetType().ToString().Contains(".Runo3."))
            {
                //if (deviceViewModel.DeviceSignature != null)
                //{
                //    if (deviceViewModel.DeviceSignature.Contains("UC") || deviceViewModel.DeviceSignature.Contains("M"))
                //    {
                //        IsThreeEnegriesShowing = true;
                //    }
                //    else
                //    {
                //        IsThreeEnegriesShowing = false;
                //    }
                //}
            }




            if (analogMeterType == null) return;
            //if (!analogMeterType.Contains(AnalogMetersEnum.МСА961.ToString())) return;
            //IsMsa961Checked = true;
            //if (!analogMeterType.Contains('%'))
            //{
            //    TransKoefA = 100;
            //    TransKoefB = 100;
            //    TransKoefC = 100;
            //}
            //var trks = analogMeterType.Replace(AnalogMetersEnum.МСА961.ToString(), "").Split('%');
            //if (trks.Length <= 2) return;
            //int trkA;
            //int trkB;
            //int trkC;
            //if (Int32.TryParse(trks[0], out trkA))
            //    TransKoefA = trkA;

            //if (Int32.TryParse(trks[1], out trkB))
            //    TransKoefB = trkB;

            //if (Int32.TryParse(trks[2], out trkC))
            //    TransKoefC = trkC;
            

        }
 




        /// <summary>
        /// отмечен ли чекбокс МСА961
        /// </summary>
        public bool IsMsa961Checked
        {
            get { return _isMSA961Checked; }
            set
            {
                if (_isMSA961Checked == value) return;
                _isMSA961Checked = value;
                OnPropertyChanged(nameof(IsMsa961Checked));

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TransKoefA
        {
            get { return _transKoef; }
            set
            {
                _transKoef = value;
                OnPropertyChanged(nameof(TransKoefA));

            }
        }



        /// <summary>
        /// 
        /// </summary>
        public int TransKoefB
        {
            get { return _transKoef; }
            set
            {
                _transKoef = value;
                OnPropertyChanged(nameof(TransKoefB));

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TransKoefC
        {
            get { return _transKoef; }
            set
            {
                _transKoef = value;
                OnPropertyChanged(nameof(TransKoefC));

            }
        }


    }


}