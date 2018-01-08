using JavierCacho.RoverExercise.ViewModel;
using JavierCacho.RoverExercise.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JavierCacho.RoverExercise.Services;

namespace JavierCacho.RoverExercise.Controller
{
    public static class MainController
    {

        public static void ProcessRequest(ViewType viewType, RoverEnvironmentConfig roverEnvironment)
        {
            try
            {
                if (viewType == ViewType.InitialView) { ProcessInitialView(); }
                else if (viewType == ViewType.AreaDimensionsView) { ProcessAreaDimensionsView(roverEnvironment); }
                else if (viewType == ViewType.RoverInitialVectorView) { ProcessInitialRoverVectorView(roverEnvironment); }
                else if (viewType == ViewType.WalkView) { ProcessWalkView(roverEnvironment); }
                else if (viewType == ViewType.ResultView) { ProcessResultView(roverEnvironment); }
            }
            catch (Exception e)
            {
                Console.WriteLine(ViewSettings.UnexpectedErrorViewMessage);
                Console.ReadLine();
            }
            
        }

        private static void ProcessInitialView ()
        {
            Console.WriteLine(ViewSettings.InitialViewMessage);
            ProcessRequest(ViewType.AreaDimensionsView, new RoverEnvironmentConfig());
        }

        private static void ProcessAreaDimensionsView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.AreaDimensionsViewMessage);
            string inputText = Console.ReadLine();
            AreaDimensions areaDimensions = ViewModel.ViewModelService.GetValidAreaDimensions(inputText);

            if (areaDimensions != null)
            {
                var inputDimensions = inputText.Trim().Split(' ');
                roverEnvironment.AreaDimensions = areaDimensions;

                ProcessRequest(ViewType.RoverInitialVectorView,roverEnvironment);
            }
            else
            {
                ProcessInvalidAreaDimensionsView(roverEnvironment);
            }
            
        }

        private static void ProcessInvalidAreaDimensionsView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.InvalidSyntaxForAreaDimensionsViewMessage);
            ProcessAreaDimensionsView(roverEnvironment);
        }

        private static void ProcessInitialRoverVectorView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.RoverIntialVectorViewMessage);
            string inputText = Console.ReadLine();
            Vector roverInitialVector = ViewModel.ViewModelService.GetValidVector(inputText);

            if (roverInitialVector != null)
            {
                roverEnvironment.RoverInitialVector = roverInitialVector;
                ProcessRequest(ViewType.WalkView, roverEnvironment);
            }
            else
            {
                ProcessInvalidRoverInitialVectorView(roverEnvironment);
            }

        }

        private static void ProcessInvalidRoverInitialVectorView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.InvalidSyntaxPositionViewMessage);
            ProcessRequest(ViewType.RoverInitialVectorView, roverEnvironment);
        }

        private static void ProcessWalkView (RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.WalkViewMessage);
            string inputText = Console.ReadLine();
            IEnumerable<MovementType> roverWalk = ViewModel.ViewModelService.GetValidWalk(inputText);

            if (roverWalk != null)
            {
                roverEnvironment.Walk = roverWalk;
                ProcessRequest(ViewType.ResultView, roverEnvironment);
            }
            else
            {
                ProcessAreaDimensionsView(roverEnvironment);
            }

        }
        private static void ProcessInvalidWalkView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.InvalidSyntaxForMovementViewMessage);
            ProcessRequest(ViewType.WalkView, roverEnvironment);
        }

        private static void ProcessResultView(RoverEnvironmentConfig roverEnvironment)
        {
            Console.WriteLine(ViewSettings.ResultViewMessage);
            Console.WriteLine(RoverExplorationService.GetResult(roverEnvironment));
            Console.WriteLine(ViewSettings.FinalViewMessage);
            Console.ReadLine();
        }




    }
}
