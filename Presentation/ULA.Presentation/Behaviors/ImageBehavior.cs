using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ULA.Presentation.Behaviors
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageBehavior : Behavior<Image>
    {


        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        ///     Вызывается после того как поведение привяжется к AssociatedObject
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
          //  (AssociatedObject as ListBox).Items
           

        }

    }
}
