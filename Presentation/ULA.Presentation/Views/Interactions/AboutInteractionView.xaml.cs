using System.IO;

namespace ULA.Presentation.Views.Interactions
{
    /// <summary>
    ///     Interaction logic for AboutInteractionView.xaml
    /// </summary>
    public partial class AboutInteractionView
    {
        /// <summary>
        ///     Create a instance of <see cref="AboutInteractionView" />
        /// </summary>
        public AboutInteractionView()
        {
            InitializeComponent();
            FileInfo f = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dateTextBox.Text = "Oт " + f.LastWriteTime;
        }
    }
}