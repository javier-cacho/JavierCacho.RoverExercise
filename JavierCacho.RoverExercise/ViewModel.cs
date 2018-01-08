using JavierCacho.RoverExercise.CoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavierCacho.RoverExercise.ViewModel
{
    public enum ViewType
    {
        InitialView,
        AreaDimensionsView,
        IncorrectAreaDimensionsView,
        RoverInitialVectorView,
        WalkView,
        ResultView,
        ExitOptionView,
        FinalView,
        ExitView,
        InvalidSyntaxForAreaDimensionsView,
        InvalidSyntaxPositionView,
        InvalidSyntaxForMovementView,
        RoverOutsideAreaView,
        UnexpectedErrorView,
    }

    public static class ViewSettings
    {
        public const string InitialViewMessage                          = "Hello to the Rover Exercise.";
        public const string AreaDimensionsViewMessage                   = "Please, type the dimentions of the bidimensional area, in the format width height:";
        public const string RoverIntialVectorViewMessage               = "Please, type the initial position (bottom left corner is 0,0) and orientation (N,S,E,W) of the Rover; e.g. 0 2 N";
        public const string WalkViewMessage                             = "Please, type the the sequence of movements for the walk -e.g. ALLRRLAA";
        public const string ResultViewMessage                           = "The result for your input is: ";
        public const string FinalViewMessage                            = "Thanks for your interest.";
        public const string InvalidSyntaxForAreaDimensionsViewMessage   = "Area dimensions must be two positive integers";
        public const string InvalidSyntaxPositionViewMessage            = "Position must be a sequence of horizontal position as positive integer, vertical position as positive integer and a letter from N,S,E,W; e.g. 2,3,S";
        public const string InvalidSyntaxForMovementViewMessage         = "Movement command must be a squence of letter from A,L,R with no spaces between them; e.g. ALLLRLAAA";
        public const string UnexpectedErrorViewMessage                  = "Sorry, there was an unexpected error";


    }
    public static class ViewModelService
    {
        public static AreaDimensions GetValidAreaDimensions(string inputText)
        {
            var inputArray = inputText.Trim().Split(' ');
            uint width = default(uint);
            uint height= default(uint);

            bool areValidDimensions = inputArray.Count() == 2 && uint.TryParse(inputArray[0], out width) && uint.TryParse(inputArray[1], out height);

            if (areValidDimensions)
            {
                return new AreaDimensions(width, height);
            }

            return null;
        }

        public static Vector GetValidVector(string inputText)
        {
            var inputArray = inputText.Trim().Split(' ');
            uint horizontalPosition = default(uint);
            uint verticalPosition = default(uint);

            bool isValidVector = 
                inputArray.Count() == 3
                && uint.TryParse(inputArray[0], out horizontalPosition)
                && uint.TryParse(inputArray[1], out verticalPosition)
                && Enum.GetNames(typeof(OrientationType))
                   .Any(orientationType =>
                      string.Equals(orientationType, inputArray[2], StringComparison.InvariantCultureIgnoreCase)
                );

            if (isValidVector)
            {
                return 
                    new Vector
                    (new Position(horizontalPosition, verticalPosition), 
                     new Orientation((OrientationType)Enum.Parse(typeof(OrientationType), inputArray[2],true)));
            }

            return null;
               
        }

        public static IEnumerable<MovementType> GetValidWalk(string inputText)
        {
            var input = inputText.Trim().ToCharArray();

            bool isValidWalk = input.Any(movement =>
                  Enum.GetNames(typeof(MovementType))
                   .Any(movementType =>
                      string.Equals(movementType, movement.ToString(), StringComparison.InvariantCultureIgnoreCase)
                    )
                 );

            if (isValidWalk)
            {
                return input.Select(character => (MovementType)Enum.Parse(typeof(MovementType), character.ToString(),true));
            }

            return null;
                
        }



    }
}
