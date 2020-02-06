using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.LoggerServices;

namespace ULA.Business.DeviceDomain
{
    public class DefectState : IDefectState
    {
        private readonly DefectLoggerService _defectLoggerService;
        private Logger _logger;
        private bool? _isManagementDefect;
        private bool? _isNoPowerDefect;
        private bool? _isProtectionDefect;
        private bool? _isFusesDefect;
        private bool? _isManagementChainsDefect;

        public DefectState(DefectLoggerService defectLoggerService)
        {
            _defectLoggerService = defectLoggerService;
        }

        #region Implementation of IDefectState

        public bool? IsManagementDefect
        {
            get { return _isManagementDefect; }
            set
            {
                if ((_isManagementDefect != null) && (_isManagementDefect == value)) return;
                if (_isManagementDefect != null)
                {
                    _defectLoggerService.ManagementDefectChanged(value.Value, _logger);
                }
                _isManagementDefect = value;
            }
        }

        public bool? IsManagementChainsDefect
        {
            get { return _isManagementChainsDefect; }
            set
            {
                if ((_isManagementChainsDefect != null) && (_isManagementChainsDefect == value)) return;
                if (_isManagementChainsDefect != null)
                {
                    _defectLoggerService.ManagmentChainsDefectChanged(value.Value, _logger);
                }
                _isManagementChainsDefect = value;
                
            }
        }

        public bool? IsFusesDefect
        {
            get { return _isFusesDefect; }
            set
            {
                if ((_isFusesDefect != null) && (_isFusesDefect == value)) return;
                if (_isFusesDefect != null)
                {
                    _defectLoggerService.FusesDefectChanged(value.Value, _logger);
                }
                _isFusesDefect = value;
                
            }
        }

        public bool? IsProtectionDefect
        {
            get { return _isProtectionDefect; }
            set
            {
                if ((_isProtectionDefect != null) && (_isProtectionDefect == value)) return;
                if (_isProtectionDefect != null)
                {
                    _defectLoggerService.ProtectionDefectChanged(value.Value, _logger);
                }
                _isProtectionDefect = value; 
            }
        }

        public bool? IsNoPowerDefect
        {
            get { return _isNoPowerDefect; }
            set
            {
                if ((_isNoPowerDefect != null) && (_isNoPowerDefect == value)) return;
                if (_isNoPowerDefect != null)
                {
                    _defectLoggerService.PowerChainsDefectChanged(value.Value, _logger);
                }
                _isNoPowerDefect = value;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasAnyDefect
        {
            get
            {

                if (IsNoPowerDefect == null ||
                    IsProtectionDefect == null ||
                    IsManagementDefect == null ||
                    IsManagementChainsDefect == null ||
                    IsFusesDefect == null) return false;

                return 
                           (IsFusesDefect.Value || IsManagementChainsDefect.Value || IsManagementDefect.Value ||
                            IsNoPowerDefect.Value ||
                            IsProtectionDefect.Value);
            }
        }

        public void SetLogger(Logger logger)
        {
            _logger = logger;
        }

        #endregion
    }
}
