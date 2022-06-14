using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using HCI_Projekat.controls;

namespace HCI_Projekat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        MainWindow prozor;
        ManagerNetworkWindow mnw;
        NewScheduleItemWindow nsiw;
        NewTrainLineWindow ntlw;
        NewTrainWindow ntw;

        public JavaScriptControlHelper(MainWindow w)
        {
            prozor = w;
        }

        public JavaScriptControlHelper(ManagerNetworkWindow w)
        {
            mnw = w;
        }

        public JavaScriptControlHelper(NewScheduleItemWindow w)
        {
            nsiw = w;
        }

        public JavaScriptControlHelper(NewTrainLineWindow w)
        {
            ntlw = w;
        }

        public JavaScriptControlHelper(NewTrainWindow w)
        {
            ntw = w;
        }

    }
}
