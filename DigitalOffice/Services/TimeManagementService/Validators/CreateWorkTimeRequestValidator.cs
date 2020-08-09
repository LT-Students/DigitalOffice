using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using LT.DigitalOffice.TimeManagementService.Models;
using LT.DigitalOffice.TimeManagementService.Repositories.Filters;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using System.Globalization;

namespace LT.DigitalOffice.TimeManagementService.Validators
{
    public class CreateWorkTimeRequestValidator : AbstractValidator<CreateWorkTimeRequest>
    {
        private IWorkTimeRepository repository;

        /// <summary>
        /// How many days ago can WorkTime be added.
        /// </summary>
        public const int FromDay = 3;
        /// <summary>
        /// How many days ahead can WorkTime be added.
        /// </summary>
        public const int ToDay = 2;
        /// <summary>
        /// Limit on working hours in a row.
        /// </summary>
        public static TimeSpan WorkingLimit { get; } = new TimeSpan(24, 0, 0);

        private readonly DateTime fromDateTime = DateTime.Now.AddDays(-FromDay);
        private readonly DateTime toDateTime = DateTime.Now.AddDays(ToDay);
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        public CreateWorkTimeRequestValidator([FromServices] IWorkTimeRepository repository)
        {
            this.repository = repository;

            RuleFor(wt => wt.WorkerUserId)
                    .NotEmpty();

            RuleFor(wt => wt.StartTime)
                .NotEqual(new DateTime())
                .Must(st => st > fromDateTime)
                .WithMessage(date => $"WorkTime had to be filled no later than {fromDateTime.ToString(culture)}.")
                .Must(st => st < toDateTime)
                .WithMessage(date => $"WorkTime cannot be filled until {toDateTime.ToString(culture)}.");

            RuleFor(wt => wt.EndTime)
                .NotEqual(new DateTime());

            RuleFor(wt => wt.ProjectId)
                .NotEmpty();

            RuleFor(wt => wt.Title)
                .NotEmpty();

            RuleFor(wt => wt)
                .Must(wt => wt.StartTime < wt.EndTime)
                .WithMessage("You cannot indicate that you worked zero hours or a negative amount.")
                .Must(wt => wt.EndTime - wt.StartTime <= WorkingLimit)
                .WithMessage(time => string.Format(
                    "You cannot indicate that you worked more than {0} hours and {1} minutes.",
                    WorkingLimit.Hours, WorkingLimit.Minutes))
                .Must(wt =>
                {
                    var oldWorkTimes = repository.GetUserWorkTimes(
                        wt.WorkerUserId,
                        new WorkTimeFilter
                        {
                            StartTime = wt.StartTime.AddMinutes(-WorkingLimit.TotalMinutes),
                            EndTime = wt.EndTime
                        });

                    foreach (var oldWorkTime in oldWorkTimes)
                    {
                        var firstNewWorkTime = wt.EndTime <= oldWorkTime.StartTime;
                        var firstOldWorkTime = oldWorkTime.EndTime <= wt.StartTime;
                        if (!(firstNewWorkTime || firstOldWorkTime))
                        {
                            return false;
                        }
                    }

                    return true;
                })
                .WithMessage("New WorkTime should not overlap with old ones.");
        }
    }
}
