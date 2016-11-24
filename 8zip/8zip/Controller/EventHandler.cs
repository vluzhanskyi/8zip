using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using _8zip.CustomEvents;

namespace _8zip.Controller
{
   public class EventHandler
    {
        public static event EventHandler<ProgressEventArgs> ProgressEvent;
        public static event EventHandler<ExtractProgressEventArgs> ExtratingProgressEvent;
        public static event EventHandler<ChangeMaxProgressValueEventArgs> MaxValueChangedEvent;
        public static event EventHandler<UpdateFormArgs> UpdateFormEvent;
        public static event EventHandler<UpdatePackageNameArgs> ChangePackageNameEvent;
        public static event EventHandler<ShowExceptionMessageArks> ExceptionEvent;

        protected virtual void OnRiseExceptionEvent(object sender, ShowExceptionMessageArks showExceptionMessageArks)
        {
            EventHandler<ShowExceptionMessageArks> handler = ExceptionEvent;

            if (handler != null)
            {
                handler(this, showExceptionMessageArks);
            }
        }

        protected virtual void OnRiseChangePackageNameEvent(UpdatePackageNameArgs updatePackageNameArgs)
        {
            EventHandler<UpdatePackageNameArgs> handler = ChangePackageNameEvent;

            if (handler != null)
            {
                handler(this, updatePackageNameArgs);
            }

        }

        protected virtual void OnRaiseUpdateFormEvent(UpdateFormArgs e)
        {
            EventHandler<UpdateFormArgs> handler = UpdateFormEvent;

            if (handler != null)
            {
                handler(this, e);
            }

        }

        internal void OnRaiseExtractProgressEvent(object sender, ExtractProgressEventArgs e)
        {
            EventHandler<ExtractProgressEventArgs> handler = ExtratingProgressEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

       public void OnProgressEvent(object sender, ProgressEventArgs progressEventArgs)
        {
            EventHandler<ProgressEventArgs> handler = ProgressEvent;

            if (handler != null)
            {
                handler(this, progressEventArgs);
            }
        }

        protected virtual void OnRaiseMaxProgressValueChangedEvent(ChangeMaxProgressValueEventArgs e)
        {
            EventHandler<ChangeMaxProgressValueEventArgs> handler = MaxValueChangedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
