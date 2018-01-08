using JavierCacho.RoverExercise.CoreEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JavierCacho.RoverExercise.Services
{
    public class RoverExplorationService
    {
        public static string GetResult(RoverEnvironmentConfig environmentConfig)
        {
            var roverFinalVector = GetFinalVector(environmentConfig);

            bool isValidPosition =
                roverFinalVector.Position.HorizontalCoordinate <= environmentConfig.AreaDimensions.Width
                && roverFinalVector.Position.VerticalCoordinate <= environmentConfig.AreaDimensions.Height;

            return
                  isValidPosition.ToString() + ", "
                + roverFinalVector.Orientation.ToString() + ", ("
                + roverFinalVector.Position.HorizontalCoordinate.ToString() + ","
                + roverFinalVector.Position.VerticalCoordinate.ToString() + ").";
        }

        private static Vector GetFinalVector(RoverEnvironmentConfig environmentConfig)
        {
            return 

                environmentConfig.Walk.Aggregate(environmentConfig.RoverInitialVector, (vector, movement) => vector + movement);
        }

        
    }
}
