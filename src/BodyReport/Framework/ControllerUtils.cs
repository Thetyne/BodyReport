﻿using BodyReport.Resources;
using Message;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyReport.Framework
{
    public static class ControllerUtils
    {
        public static List<SelectListItem> CreateSelectRoleItemList(List<Role> roleList, string currentUserId)
        {
            var result = new List<SelectListItem>();

            foreach (Role role in roleList)
            {
                result.Add(new SelectListItem { Text = role.Name, Value = role.Id, Selected = currentUserId == role.Id });
            }

            return result;
        }

        public static List<SelectListItem> CreateSelectMuscularGroupItemList(List<MuscularGroup> muscularGroupList, int currentId, bool addNotSelectedValue = false)
        {
            var result = new List<SelectListItem>();
            if (addNotSelectedValue)
                result.Add(new SelectListItem { Text = Translation.NOT_SPECIFIED, Value = "0", Selected = currentId == 0 });
            
            if (muscularGroupList == null)
                return result;
            
            muscularGroupList = muscularGroupList.OrderBy(m => m.Name).ToList();

            
            foreach (MuscularGroup muscularGroup in muscularGroupList)
            {
                result.Add(new SelectListItem { Text = muscularGroup.Name, Value = muscularGroup.Id.ToString(), Selected = currentId == muscularGroup.Id });
            }

            return result;
        }

        public static List<SelectListItem> CreateSelectMuscleItemList(List<Muscle> muscleList, int currentId, bool addNotSelectedValue = false)
        {
            var result = new List<SelectListItem>();

            if (addNotSelectedValue)
                result.Add(new SelectListItem { Text = Translation.NOT_SPECIFIED, Value = "0", Selected = currentId == 0 });


            if (muscleList == null)
                return result;

            muscleList = muscleList.OrderBy(m => m.Name).ToList();
           
            foreach (Muscle muscle in muscleList)
            {
                result.Add(new SelectListItem { Text = muscle.Name, Value = muscle.Id.ToString(), Selected = currentId == muscle.Id });
            }

            return result;
        }

        public static List<SelectListItem> CreateSelectBodyExerciseItemList(List<BodyExercise> bodyExerciseList, int currentId, bool addNotSelectedValue = false)
        {
            var result = new List<SelectListItem>();

            if (bodyExerciseList == null)
                return result;

            bodyExerciseList = bodyExerciseList.OrderBy(m => m.Name).ToList();
            
            if (addNotSelectedValue)
                result.Add(new SelectListItem { Text = Translation.NOT_SPECIFIED, Value = "0", Selected = currentId == 0 });

            foreach (BodyExercise bodyExercise in bodyExerciseList)
            {
                result.Add(new SelectListItem { Text = bodyExercise.Name, Value = bodyExercise.Id.ToString(), Selected = currentId == bodyExercise.Id });
            }

            return result;
        }

        public static List<SelectListItem> CreateSelectSexItemList(int sexId)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = Translation.MAN, Value = ((int)TSexType.MAN).ToString(), Selected = sexId == (int)TSexType.MAN });
            result.Add(new SelectListItem { Text = Translation.WOMAN, Value = ((int)TSexType.WOMAN).ToString(), Selected = sexId == (int)TSexType.WOMAN });
            return result;
        }

        public static List<SelectListItem> CreateSelectUnitItemList(int unitId)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = Translation.IMPERIAL, Value = ((int)TUnitType.Imperial).ToString(), Selected = unitId == (int)TUnitType.Imperial });
            result.Add(new SelectListItem { Text = Translation.METRIC, Value = ((int)TUnitType.Metric).ToString(), Selected = unitId == (int)TUnitType.Metric });
            return result;
        }

        public static List<SelectListItem> CreateSelectCountryItemList(List<Country> countryList, int userCountryId)
        {
            var result = new List<SelectListItem>();

            result.Add(new SelectListItem { Text = Translation.NOT_SPECIFIED, Value = "0", Selected = userCountryId == 0 });

            foreach (var country in countryList)
            {
                result.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString(), Selected = userCountryId == country.Id });
            }

            return result;
        }
    }
}
