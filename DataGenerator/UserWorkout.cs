using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataGenerator
{
    class UserWorkout
    {
        public int id;
        public int userID;
        public int workoutID;
        public string startDate;
        public TimeSpan startTime;


    }
}
