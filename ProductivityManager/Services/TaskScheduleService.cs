using ProductivityManager.Models.Task;

namespace ProductivityManager.Services;

public class TaskScheduleService
{
    public List<TaskOccurrence> GetOccurrences(
        TaskModel task,
        DateTime rangeStart,
        DateTime rangeEnd)
    {
        if (task is WeeklyTaskModel weeklyTask)
        {
            return GetWeeklyOccurrences(
                weeklyTask,
                rangeStart,
                rangeEnd
            );
        }

        if (task is MonthlyTaskModel monthlyTask)
        {
            return GetMonthlyOccurrences(
                monthlyTask,
                rangeStart,
                rangeEnd
            );
        }

        return GetDailyOccurrences(
            task,
            rangeStart,
            rangeEnd
        );
    }
    
    private List<TaskOccurrence> GetDailyOccurrences(
        TaskModel task,
        DateTime rangeStart,
        DateTime rangeEnd)
    {
        List<TaskOccurrence> occurrences = new();

        DateTime currentDate = task.StartDateTime.Date;

        while (currentDate <= task.EndDateTime.Date)
        {
            DateTime occurrenceStart =
                currentDate + task.StartDateTime.TimeOfDay;

            DateTime occurrenceEnd =
                currentDate + task.EndDateTime.TimeOfDay;

            if (occurrenceEnd >= rangeStart &&
                occurrenceStart <= rangeEnd)
            {
                occurrences.Add(
                    new TaskOccurrence(
                        task,
                        occurrenceStart,
                        occurrenceEnd
                    )
                );
            }

            currentDate = currentDate.AddDays(1);
        }

        return occurrences;
    }

    private List<TaskOccurrence> GetWeeklyOccurrences(WeeklyTaskModel task, DateTime rangeStart, DateTime rangeEnd)
    {
        List<TaskOccurrence> occurrences = new();

        TimeSpan duration = task.EndDateTime - task.StartDateTime;

        DateTime currentDate = rangeStart.Date;
        
        while (currentDate <= rangeEnd.Date && currentDate <= task.RepeatUntil.Date)
        {
            if (currentDate >= task.StartDateTime.Date && task.ScheduledDays.Contains(currentDate.DayOfWeek))
            {
                DateTime occurrenceStart = currentDate + task.StartDateTime.TimeOfDay;

                DateTime occurrenceEnd = occurrenceStart + duration;

                if (occurrenceEnd >= rangeStart && occurrenceStart <= rangeEnd)
                {
                    occurrences.Add(new TaskOccurrence(task, occurrenceStart, occurrenceEnd));
                }
            }

            currentDate = currentDate.AddDays(1);
        }

        return occurrences;
    }

    private List<TaskOccurrence> GetMonthlyOccurrences(
        MonthlyTaskModel task,
        DateTime rangeStart,
        DateTime rangeEnd)
    {
        List<TaskOccurrence> occurrences = new();

        TimeSpan duration =
            task.EndDateTime - task.StartDateTime;

        DateTime currentMonth =
            new DateTime(rangeStart.Year, rangeStart.Month, 1);

        while (currentMonth <= rangeEnd.Date &&
               currentMonth <= task.RepeatUntil.Date)
        {
            int daysInMonth = DateTime.DaysInMonth(
                currentMonth.Year,
                currentMonth.Month
            );

            if (task.DayOfMonth <= daysInMonth)
            {
                DateTime occurrenceDate = new DateTime(
                    currentMonth.Year,
                    currentMonth.Month,
                    task.DayOfMonth
                );

                if (occurrenceDate >= task.StartDateTime.Date &&
                    occurrenceDate <= task.RepeatUntil.Date)
                {
                    DateTime occurrenceStart =
                        occurrenceDate + task.StartDateTime.TimeOfDay;

                    DateTime occurrenceEnd =
                        occurrenceStart + duration;

                    if (occurrenceEnd >= rangeStart &&
                        occurrenceStart <= rangeEnd)
                    {
                        occurrences.Add(new TaskOccurrence(
                            task,
                            occurrenceStart,
                            occurrenceEnd
                        ));
                    }
                }
            }

            currentMonth = currentMonth.AddMonths(1);
        }

        return occurrences;
    }
}