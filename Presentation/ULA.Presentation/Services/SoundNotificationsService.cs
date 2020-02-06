using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ULA.Presentation.Infrastructure.Services;

namespace ULA.Presentation.Services
{
    /// <summary>
    /// сервис звуков
    /// </summary>
    public class SoundNotificationsService : ISoundNotificationsService
    {
        private ICollection<object> _notifyingObjects = new List<object>();
        private Thread _soundPlayingThread;
        private bool _isSoundActivated;
        /// <summary>
        /// сервис звуков
        /// </summary>
        public SoundNotificationsService()
        {
            _soundPlayingThread = new Thread(PlaySound);
        }

        #region Implementation of ISoundNotificationsService
        /// <summary>
        /// добавить объект
        /// </summary>
        public void SetNotificationSource(object notifyingObject)
        {
            if (!_notifyingObjects.Contains(notifyingObject))
            {
                _notifyingObjects.Add(notifyingObject);
            }
            NotificationsChanged();
        }
        /// <summary>
        /// освободить объект
        /// </summary>
        public void ReleaseNotificationSource(object notifyingObject)
        {
            if (_notifyingObjects.Contains(notifyingObject))
            {
                _notifyingObjects.Remove(notifyingObject);
            }
            NotificationsChanged();
        }
        /// <summary>
        /// прекратить воспроизведение всех звуков
        /// </summary>
        public void ReleaseAllAndStopSound()
        {
            _notifyingObjects.Clear();
            NotificationsChanged();
        }


        private void NotificationsChanged()
        {

            if (_notifyingObjects.Count > 0)
            {
                _isSoundActivated = true;
                if (_soundPlayingThread.ThreadState == ThreadState.Running)
                {
                    return;

                }
                if (_soundPlayingThread.ThreadState == ThreadState.Unstarted)
                {
                    _soundPlayingThread.Start();
                }
                else if ((_soundPlayingThread.ThreadState == ThreadState.Stopped) ||
                         (_soundPlayingThread.ThreadState == ThreadState.Aborted))
                {
                    _soundPlayingThread = new Thread(PlaySound);
                    _soundPlayingThread.Start();
                }
            }
            else
            {
                _isSoundActivated = false;
            }
        }

        private void PlaySound()
        {
            while (_isSoundActivated)
            {
                System.Media.SystemSounds.Beep.Play();
                Thread.Sleep(3000);
            }
        }

        #endregion
    }
}
