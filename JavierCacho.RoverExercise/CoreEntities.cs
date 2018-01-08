using System;
using System.Collections.Generic;
using System.Text;

namespace JavierCacho.RoverExercise.CoreEntities
{
    public enum OrientationType
    {
        N,
        W,
        S,
        E
    }

    public enum MovementType
    {
        A,
        L,
        R
    }

    public class RoverEnvironmentConfig
    {
        public AreaDimensions AreaDimensions { get; set; }
        public Vector RoverInitialVector { get; set; }
        public IEnumerable<MovementType> Walk { get; set; }
    }

    public class AreaDimensions
    {
        public readonly uint Width;
        public readonly uint Height;

        public AreaDimensions(uint width, uint height)
        {
            this.Width = width;
            this.Height = height;
        }
    }

    public class Position
    {
        public readonly uint HorizontalCoordinate;
        public readonly uint VerticalCoordinate;

        public Position(uint horizontalCoordinate, uint verticalCoordinate)
        {
            this.HorizontalCoordinate = horizontalCoordinate;
            this.VerticalCoordinate = verticalCoordinate;
        }
    }


    public class Orientation
    {
        public readonly OrientationType OrientationType;

        public Orientation(OrientationType orientationType)
        {
            this.OrientationType = orientationType;
        }

        public static Orientation operator +(Orientation orientation, int rotation) =>
          new Orientation
            (
              orientation.OrientationType
              + (rotation % Enum.GetNames(typeof(OrientationType)).Length)
              % Enum.GetNames(typeof(OrientationType)).Length
            );
        public override string ToString()
        {
            return this.OrientationType.ToString();
        }
    }

    public class Vector
    {
        public Vector(Position position, Orientation orientation)
        {
            this.Position = position;
            this.Orientation = orientation;
        }

        public readonly Position Position;
        public readonly Orientation Orientation;

        public static Vector operator +(Vector currentVector, MovementType movementType)
        {
            if (movementType == MovementType.A && currentVector.Orientation.OrientationType == OrientationType.N)
            {
                return new Vector(new Position(currentVector.Position.HorizontalCoordinate, currentVector.Position.VerticalCoordinate + 1), currentVector.Orientation);
            }
            else if (movementType == MovementType.A && currentVector.Orientation.OrientationType == OrientationType.E)
            {
                return new Vector(new Position(currentVector.Position.HorizontalCoordinate + 1, currentVector.Position.VerticalCoordinate), currentVector.Orientation);
            }
            else if (movementType == MovementType.A && currentVector.Orientation.OrientationType == OrientationType.S)
            {
                return new Vector(new Position(currentVector.Position.HorizontalCoordinate, currentVector.Position.VerticalCoordinate - 1), currentVector.Orientation);
            }
            else if (movementType == MovementType.A && currentVector.Orientation.OrientationType == OrientationType.W)
            {
                return new Vector(new Position(currentVector.Position.HorizontalCoordinate - 1, currentVector.Position.VerticalCoordinate), currentVector.Orientation);
            }
            else if (movementType == MovementType.L)
            {
                return new Vector(currentVector.Position, currentVector.Orientation + (-1));
            }
            else if (movementType == MovementType.R)
            {
                return new Vector(currentVector.Position, currentVector.Orientation + 1);
            }
            else
            {
                return new Vector(currentVector.Position, currentVector.Orientation);
            }
        }

    }
}
