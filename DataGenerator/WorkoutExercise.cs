using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    class WorkoutExercise
    {
        public int id;
        public int workoutID;
        public int exerciseID;
        public int exerciseOrder;
        public static TimeSpan setTime=TimeSpan.Parse("00:00:45");
        public static int setTimeSeconds = 45;
        public string startDate;
        public string endDate;
        public TimeSpan startTime;
        public TimeSpan endTime;
        public static int setNumber = 3;
        public static TimeSpan restTime = TimeSpan.Parse("00:00:30");
    }
}
