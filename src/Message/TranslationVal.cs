﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message
{
    public class TranslationValKey
    {
        /// <summary>
        /// Culture Id
        /// </summary>
        public int CultureId { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
    }

    public class TranslationVal : TranslationValKey
    {
        public string Value { get; set; }
    }

    public class TranslationValCriteria : CriteriaField
    {
        /// <summary>
        /// Culture Id
        /// </summary>
        public IntegerCriteria CultureId { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public StringCriteria Key { get; set; }
    }
}
