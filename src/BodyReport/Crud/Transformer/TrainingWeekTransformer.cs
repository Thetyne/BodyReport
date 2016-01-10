﻿using BodyReport.Models;
using Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.Crud.Transformer
{
    public class TrainingWeekTransformer
    {
        public static void ToRow(TrainingWeek bean, TrainingWeekRow row)
        {
            if (bean == null)
                return;

            row.UserId = bean.UserId;
            row.Year = bean.Year;
            row.WeekOfYear = bean.WeekOfYear;
            row.UserHeight = bean.UserHeight;
            row.UserWeight = bean.UserWeight;
        }

        internal static TrainingWeek ToBean(TrainingWeekRow row)
        {
            if (row == null)
                return null;

            var bean = new TrainingWeek();
            bean.UserId = row.UserId;
            bean.Year = row.Year;
            bean.WeekOfYear = row.WeekOfYear;
            bean.UserHeight = row.UserHeight;
            bean.UserWeight = row.UserWeight;
            return bean;
        }
    }
}