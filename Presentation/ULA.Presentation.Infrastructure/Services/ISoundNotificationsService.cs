namespace ULA.Presentation.Infrastructure.Services
{
    /// <summary>
    /// сервис звуков
    /// </summary>
    public interface ISoundNotificationsService
    {
        /// <summary>
        /// добавить объект
        /// </summary>
        /// <param name="notifyingObject"></param>
        void SetNotificationSource(object notifyingObject);
        /// <summary>
        /// освободить объект
        /// </summary>
        /// <param name="notifyingObject"></param>
        void ReleaseNotificationSource(object notifyingObject);

        /// <summary>
        /// прекратить воспроизведение всех звуков
        /// </summary>
        void ReleaseAllAndStopSound();

    }
}