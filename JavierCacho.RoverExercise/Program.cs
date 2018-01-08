namespace JavierCachoRoverExercise
{
    using JavierCacho.RoverExercise.Controller;
    using JavierCacho.RoverExercise.CoreEntities;
    using JavierCacho.RoverExercise.ViewModel;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = typeof(Program).Name;
            ViewType viewType = ViewType.InitialView;
            MainController.ProcessRequest(viewType, new RoverEnvironmentConfig());
        }
    }
}
