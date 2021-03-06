﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BodyReport.Areas.User.ViewModels;
using BodyReport.Framework;
using BodyReport.Models;
using BodyReport.Resources;
using BodyReport.Message;
using BodyReport.Message.Web;
using BodyReport.Message.Web.MultipleParameters;
using Microsoft.AspNetCore.Authorization;
using BodyReport.ServiceLayers.Interfaces;

namespace BodyReport.Areas.Api.Controllers
{
    [Area("Api")]
    [Authorize]
    public class TrainingDaysController : MvcController
    {
        /// <summary>
        /// Service layer TrainingDays
        /// </summary>
        private readonly ITrainingDaysService _trainingDaysService;
        /// <summary>
        /// Service layer TrainingDays
        /// </summary>
        private readonly ITrainingWeeksService _trainingWeeksService;
        /// <summary>
        /// Service layer TrainingDays
        /// </summary>
        private readonly IUserInfosService _userInfosService;

        public TrainingDaysController(UserManager<ApplicationUser> userManager,
                                      ITrainingDaysService trainingDaysService,
                                      ITrainingWeeksService trainingWeeksService,
                                      IUserInfosService userInfosService) : base(userManager)
        {
            _trainingDaysService = trainingDaysService;
            _trainingWeeksService = trainingWeeksService;
            _userInfosService = userInfosService;
        }

        // Get api/TrainingDays/Get
        [HttpGet]
        public IActionResult Get(TrainingDayKey trainingDayKey, TrainingDayScenario trainingDayScenario)
        {
            try
            {
                if (trainingDayKey == null || trainingDayScenario == null)
                    return BadRequest();
                var trainingday = _trainingDaysService.GetTrainingDay(trainingDayKey, trainingDayScenario);
                return new OkObjectResult(trainingday);
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            }
        }

        // Post api/TrainingDays/Find
        [HttpPost]
        public IActionResult Find([FromBody]TrainingDayFinder trainingDayFinder)
        {
            try
            {
                if (trainingDayFinder == null)
                    return BadRequest();

                var trainingDayCriteria = trainingDayFinder.TrainingDayCriteria;
                var trainingDayScenario = trainingDayFinder.TrainingDayScenario;

                if (trainingDayCriteria == null || trainingDayCriteria.UserId == null)
                    return BadRequest();

                var result = _trainingDaysService.FindTrainingDay(AppUtils.GetUserUnit(_userInfosService, SessionUserId), trainingDayCriteria, trainingDayScenario);
                return new OkObjectResult(result); // List<TrainingDay>
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            }
        }

        // Post api/TrainingDays/Create
        [HttpPost]
        public IActionResult Create([FromBody]TrainingDayViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(viewModel.UserId) || viewModel.Year == 0 || viewModel.WeekOfYear == 0 ||
                        viewModel.DayOfWeek < 0 || viewModel.DayOfWeek > 6 || SessionUserId != viewModel.UserId)
                        return BadRequest();

                    //Verify trainingWeek exist
                    var trainingWeekKey = new TrainingWeekKey()
                    {
                        UserId = viewModel.UserId,
                        Year = viewModel.Year,
                        WeekOfYear = viewModel.WeekOfYear
                    };
                    var trainingWeekScenario = new TrainingWeekScenario() { ManageTrainingDay = false };
                    var trainingWeek = _trainingWeeksService.GetTrainingWeek(trainingWeekKey, trainingWeekScenario);

                    if (trainingWeek == null)
                        return BadRequest(new WebApiException(string.Format(Translation.P0_NOT_EXIST, Translation.TRAINING_WEEK)));

                    //Verify valid week of year
                    if (viewModel.WeekOfYear > 0 && viewModel.WeekOfYear <= 52)
                    {
                        var trainingDay = ControllerUtils.TransformViewModelToTrainingDay(viewModel);
                        trainingDay = _trainingDaysService.CreateTrainingDay(trainingDay);
                        if (trainingDay == null)
                            return BadRequest(new WebApiException(string.Format(Translation.IMPOSSIBLE_TO_CREATE_P0, Translation.TRAINING_DAY)));
                        else
                            return new OkObjectResult(trainingDay);
                    }
                    else
                        return BadRequest();
                }
                else
                    return BadRequest(new WebApiException(ControllerUtils.GetModelStateError(ModelState)));
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            }
        }

        // Post api/TrainingDays/Update
        [HttpPost]
        public IActionResult Update([FromBody]TrainingDayWithScenario trainingDayWithScenario)
        {
            try
            {
                if (trainingDayWithScenario == null || trainingDayWithScenario.TrainingDay == null || trainingDayWithScenario.TrainingDayScenario == null)
                    return BadRequest();

                var trainingDay = trainingDayWithScenario.TrainingDay;
                var trainingDayScenario = trainingDayWithScenario.TrainingDayScenario;
                if (string.IsNullOrWhiteSpace(trainingDay.UserId) || trainingDay.Year == 0 || trainingDay.WeekOfYear == 0 ||
                    trainingDay.DayOfWeek < 0 || trainingDay.DayOfWeek > 6 || SessionUserId != trainingDay.UserId)
                    return BadRequest();


                //Verify valid week of year
                if (trainingDay.WeekOfYear > 0 && trainingDay.WeekOfYear <= 52)
                {
                    trainingDay = _trainingDaysService.UpdateTrainingDay(trainingDay, trainingDayScenario);
                    if (trainingDay == null)
                        return BadRequest(new WebApiException(string.Format(Translation.IMPOSSIBLE_TO_UPDATE_P0, Translation.TRAINING_DAY)));
                    else
                        return new OkObjectResult(trainingDay);
                }
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            }
        }

        // Post api/TrainingDays/Delete
        [HttpPost]
        public IActionResult Delete([FromBody]TrainingDayKey trainingDayKey)
        {
            try
            {
                if (trainingDayKey == null || string.IsNullOrWhiteSpace(trainingDayKey.UserId) || trainingDayKey.Year == 0)
                    return BadRequest();

                //Verify valid week of year
                if (trainingDayKey.WeekOfYear > 0 && trainingDayKey.WeekOfYear <= 52)
                {
                    _trainingDaysService.DeleteTrainingDay(trainingDayKey);
                    return new OkObjectResult(true); // Boolean
                }
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            }
        }

        // Post api/TrainingDays/SwitchDay
        [HttpPost]
        public IActionResult SwitchDay([FromBody]SwitchDayParameter switchDayParameter)
        {
            if(switchDayParameter == null || switchDayParameter.TrainingDayKey == null)
                return BadRequest();
            try
            {
                TrainingDayKey trainingDayKey = switchDayParameter.TrainingDayKey;
                int switchDayOfWeek = switchDayParameter.SwitchDayOfWeek;

                if (trainingDayKey == null || string.IsNullOrWhiteSpace(trainingDayKey.UserId) ||
                    trainingDayKey.Year == 0 || trainingDayKey.WeekOfYear == 0 || trainingDayKey.DayOfWeek < 0 || trainingDayKey.DayOfWeek > 6 ||
                    switchDayOfWeek < 0 || switchDayOfWeek > 6 || SessionUserId != trainingDayKey.UserId)
                    return BadRequest();

                _trainingDaysService.SwitchDayOnTrainingDay(trainingDayKey.UserId, trainingDayKey.Year, trainingDayKey.WeekOfYear, trainingDayKey.DayOfWeek, switchDayOfWeek);
                return new OkObjectResult(true); //bool
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            };
        }

        // Post api/TrainingDays/CopyDay
        [HttpPost]
        public IActionResult CopyDay([FromBody]CopyDayParameter copyDayParameter)
        {
            if (copyDayParameter == null || copyDayParameter.TrainingDayKey == null)
                return BadRequest();
            try
            {
                TrainingDayKey trainingDayKey = copyDayParameter.TrainingDayKey;
                int copyDayOfWeek = copyDayParameter.CopyDayOfWeek;

                if (trainingDayKey == null || string.IsNullOrWhiteSpace(trainingDayKey.UserId) ||
                    trainingDayKey.Year == 0 || trainingDayKey.WeekOfYear == 0 || trainingDayKey.DayOfWeek < 0 || trainingDayKey.DayOfWeek > 6 ||
                    copyDayOfWeek < 0 || copyDayOfWeek > 6 || SessionUserId != trainingDayKey.UserId)
                    return BadRequest();

                _trainingDaysService.CopyDayOnTrainingDay(trainingDayKey.UserId, trainingDayKey.Year, trainingDayKey.WeekOfYear, trainingDayKey.DayOfWeek, copyDayOfWeek);
                return new OkObjectResult(true); //bool
            }
            catch (Exception exception)
            {
                return BadRequest(new WebApiException("Error", exception));
            };
        }
    }
}
