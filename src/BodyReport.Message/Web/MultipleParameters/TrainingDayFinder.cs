﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.Message.Web.MultipleParameters
{
    public class TrainingDayFinder
    {
        public TrainingDayCriteria TrainingDayCriteria { get; set; }
        public TrainingDayScenario TrainingDayScenario { get; set; }

        public TrainingDayFinder()
        { }
    }
}
