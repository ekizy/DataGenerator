using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataGenerator
{
    class Generator
    {
        public SqlDBConfig dbConfig ;
        public  Random rnd;
        public static string beginningDay = "29.04.2017";
        public Generator()
        {
        dbConfig = new SqlDBConfig();
        rnd = new Random();
        }
        public void generateUser()
        {
            Console.WriteLine("Please write the new user's name");
            string username = Console.ReadLine();
            dbConfig.connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbConfig.con;
            string query = "INSERT INTO USERS (USERNAME) VALUES ('" + username + "');";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            dbConfig.breakConnection();

        }

        public void generateWorkout()
        {
            Console.WriteLine("Please write the  new workout's name");
            string workoutname = Console.ReadLine();
            dbConfig = new SqlDBConfig();
            dbConfig.connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbConfig.con;
            string query = "INSERT INTO WORKOUTS (NAME) VALUES('" + workoutname + "');";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            dbConfig.breakConnection();
        }

        public void generateUserWorkout()
        {
            try
            {
               /* List<int> userIDList = dbConfig.getUserIDs();
                int randomIndex = rnd.Next(userIDList.Count);
                int randomUserID = userIDList[randomIndex];

                List<int> workoutIDList = dbConfig.getWorkoutIDs();
                randomIndex = rnd.Next(workoutIDList.Count);
                int randomWorkoutID = workoutIDList[randomIndex]; random seçilme senaryosu şimdilik yorum*/

                generateUser();
                generateWorkout();

                List<int> workoutList = dbConfig.getWorkoutIDs();
                int workoutID = workoutList.Max();

                List<int> userList = dbConfig.getUserIDs();
                int userID = userList.Max();

                Console.WriteLine("User Id:" + userID + "\nWorkout Id:" + workoutID);

                TimeSpan start = TimeSpan.FromHours(9);
                TimeSpan end = TimeSpan.FromHours(22);
                int maxMinutes = (int)((end - start).TotalMinutes);

                int minutes = rnd.Next(maxMinutes);
                TimeSpan workoutStartTime = start.Add(TimeSpan.FromMinutes(minutes));

                String randomStartDate = beginningDay +" "+ workoutStartTime.ToString();

                Console.WriteLine("Start Date: " + randomStartDate+"\n");

                UserWorkout userWorkout = new UserWorkout();
                userWorkout.workoutID = workoutID;
                userWorkout.userID = userID;
                userWorkout.startDate = randomStartDate;
                userWorkout.startTime = workoutStartTime;

               dbConfig.connectToDB();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = dbConfig.con;
                string query = "INSERT INTO USERWORKOUTS (user,workout,start_date) VALUES ("
                    + userWorkout.userID.ToString() + "," + userWorkout.workoutID.ToString() 
                    + ",'" + userWorkout.startDate + "');";
                
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                dbConfig.breakConnection();

                generateWorkoutExercises(userWorkout);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Null Reference Exception occured.");
            }


        }

        public void generateWorkoutExercises(UserWorkout userWorkout)
        {
            List<int> exerciseIDList = dbConfig.getExerciseIDs();
            TimeSpan time = userWorkout.startTime;
            int order = 1;

            dbConfig.connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = dbConfig.con;

            for(int i=0;i<5;i++)
            {
                int randomIndex = rnd.Next(exerciseIDList.Count);
                int randomExerciseID = exerciseIDList[randomIndex];

                WorkoutExercise workoutExercise = new WorkoutExercise();
                workoutExercise.exerciseID = randomExerciseID;
                workoutExercise.workoutID = userWorkout.workoutID;
                workoutExercise.startDate = beginningDay + " "+time.ToString();
                workoutExercise.startTime = time;
                workoutExercise.endTime = time 
                    + calculateExerciseTime(WorkoutExercise.setNumber,WorkoutExercise.setTime);
                workoutExercise.endDate = beginningDay + " " + workoutExercise.endTime.ToString();
                workoutExercise.exerciseOrder = order;

                order++;
                exerciseIDList.Remove(randomExerciseID);
                time = workoutExercise.endTime + WorkoutExercise.restTime;


                string query = "INSERT INTO WORKOUTEXERCISES"+" (workout,exercise,exercise_order,"
                    +"set_number,set_time,"
                    +"start_date,end_date) VALUES ("
                    + workoutExercise.workoutID+ "," + workoutExercise.exerciseID+","+workoutExercise.exerciseOrder
                    + "," +WorkoutExercise.setNumber+","+WorkoutExercise.setTimeSeconds+",'"+workoutExercise.startDate
                    +"','"+workoutExercise.endDate+"');";

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                

            }
            dbConfig.breakConnection();
        }

        public TimeSpan calculateExerciseTime(int setNumber,TimeSpan setTime)
        {
            TimeSpan exerciseTime = TimeSpan.Parse("00:00:00");
            for(int i=0;i<setNumber;i++)
            {
                exerciseTime = exerciseTime + setTime;
            }

            return exerciseTime;
        }
    }
}
