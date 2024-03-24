using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaptersMobileApp.Models
{
    public class ActivityGroup : List<Activity>
    {
        public DateTimeOffset CreatedAt { get; private set; }

        public ActivityGroup(DateTimeOffset createdAt, List<Activity> activities) : base(activities)
        {
            CreatedAt = createdAt;
        }
    }
}
