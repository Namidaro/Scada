using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ULA.Presentation.Views.Image
{/// <summary>
/// 
/// </summary>
    public class MoveThumb : Thumb
    {
        /// <summary>
        /// 
        /// </summary>
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Control item = this.DataContext as Control;
            //Canvas canvas = VisualTreeHelper.GetParent(item) as Canvas;
            if (item != null)
            {
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);
                double newPoint = left + e.HorizontalChange > 0 ? left + e.HorizontalChange : 0;
                Canvas.SetLeft(item, newPoint);
                newPoint = top + e.VerticalChange > 0 ? top + e.VerticalChange : 0;
                Canvas.SetTop(item, newPoint);
            }
        }
    }
}
