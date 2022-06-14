using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkSharp.FeatureTouring;
using ThinkSharp.FeatureTouring.ViewModels;

namespace HCI_Projekat
{
    class CustomTourViewModel : TourViewModel
    {
        public CustomTourViewModel(ITourRun run)
        : base(run) { }

        public string ButtonText => "Наредни >>";
        public string Steps => "Корак " + CurrentStepNo + "/" + TotalStepsCount;
    }
}
