﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.WebApi.MultipleParameters
{
    public class TrainingWeekWithScenario
    {
        public TrainingWeek TrainingWeek { get; set; }
        public TrainingWeekScenario TrainingWeekScenario { get; set; }

        public TrainingWeekWithScenario()
        { }
    }
}