using BackupManager.Domain.Enumerations;
using System.Windows;
using System.Windows.Controls;

namespace BackupManager.App.Templates
{
    public class ScheduleDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item != null && item is DateInterval dateInterval)
            {
                if (dateInterval == DateInterval.Day)
                    return
                        element.FindResource("DateIntervalDayTemplate") as DataTemplate;
                else
                    return
                        element.FindResource("DateIntervalWeekTemplate") as DataTemplate;
            }

            return null;
        }
    }
}