namespace ULA.AddinsHost.ViewModel.Device.CustomItems
{
    public interface ICascadeViewModel
    {
        string Description { get; set; }
        bool? State { get; set; }
        object Model { get; set; }
        void Update();
        void SetLostConnaction();
    }
}