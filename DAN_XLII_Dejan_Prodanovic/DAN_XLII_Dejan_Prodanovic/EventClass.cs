using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic
{
    class EventClass
    {
        public delegate void ActionPerformedEventHandler(object source, TextToFileEventArgs args);
        public event ActionPerformedEventHandler ActionPerformed;

        public void OnActionPerformed(string textToFile)
        {
            if (ActionPerformed != null)
            {
                ActionPerformed(this, new TextToFileEventArgs() {TextForFile = textToFile });
            }
        }
    }
}
