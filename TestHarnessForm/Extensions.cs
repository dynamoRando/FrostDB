using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TestHarnessForm
{
    public static class Extensions
    {
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            // See Update 2 for edits Mike de Klerk suggests to insert here.

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
