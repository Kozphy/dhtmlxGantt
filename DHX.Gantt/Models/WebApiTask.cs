﻿namespace DHX.Gantt.Models
{
    public class WebApiTask
    {
        public int id { get; set; }
        public string? text { get; set; }
        public string? start_date { get; set; }
        public int duration { get; set; }
        public decimal progress { get; set; }
        public int? parent { get; set; }
        public string? type { get; set; }
        public bool open
        {
            get { return true; }
            set { }
        }

        public static explicit operator WebApiTask(Task task)
        {
            return new WebApiTask()
            {
                id = task.Id,
                text = task.Text,
                start_date = task.StartDate.ToString("yyyy-MM-dd HH:mm"),
                duration = task.Duration,
                parent = task.ParentId,
                type=task.Type,
                progress = task.Progress
            };
        }

        public static explicit operator Task(WebApiTask task)
        {
            return new Task()
            {
                Id = task.id,
                Text = task.text,
                StartDate = task.start_date != null
                    ? DateTime.Parse(task.start_date, System.Globalization.CultureInfo.InvariantCulture)
                    : new DateTime(),
                Duration = task.duration,
                ParentId = task.parent,
                Type = task.type,
                Progress = task.progress,
            };
        }
    }

}
