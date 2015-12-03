﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message
{
    public class BodyExerciseKey
    {
        /// <summary>
        /// Exercise Id
        /// </summary>
        public int Id { get; set; } = 0;
    }

    public class BodyExercise : BodyExerciseKey
    {
        /// <summary>
        /// Exercise Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Exercise Picture name
        /// </summary>
        public string ImageName { get; set; } = string.Empty;
        /// <summary>
        /// Muscular Group Id
        /// </summary>
        public int MuscularGroupId { get; set; } = 0;
    }
}
