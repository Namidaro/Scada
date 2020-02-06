namespace ULA.Business.Infrastructure.TimerInterrogation
{
    public interface IDeviceTimerInterrogationService:ITimerInterrigationService
    {
        void SetIsFullUpdating(bool isFull);
    }
}