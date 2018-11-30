using System.Drawing;

namespace MarsRover
{
    public class TurningCircle
    {
        // Member Data
        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        private const int m_num_of_directions = 4;
        private Direction m_current_direction;

        // Functions

        // Constructor
        public TurningCircle(string initial_direction)
        {
            m_current_direction = new Direction();
            InitialiseDirection(initial_direction);
        }

        // Compares the string for the initial direction of the circle
        // Using string as input as instruction is sent with a space gap
        private void InitialiseDirection(string initial_direction)
        {
            switch (initial_direction)
            {
                case "N":
                    m_current_direction = Direction.North;
                    break;
                case "E":
                    m_current_direction = Direction.East;
                    break;
                case "S":
                    m_current_direction = Direction.South;
                    break;
                case "W":
                default:
                    m_current_direction = Direction.West;
                    break;
            }
        }

        // Gets the co-ordinates to modify a position of a vehicle from on moving 1 forward in the current direction it is facing
        public Point GetModLocation()
        {
            switch (m_current_direction)
            {
                case Direction.North:
                    return new Point(0, 1);
                case Direction.East:
                    return new Point(1, 0);
                case Direction.South:
                    return new Point(0, -1);
                case Direction.West:
                    return new Point(-1, 0);
                default:
                    return new Point(0, 0);
            }
        }

        // Gets the co-ordinates to modify a position of a vehicle from on moving 1 forward in the current direction it is facing
        public string GetDirectionIdentifier()
        {
            switch (m_current_direction)
            {
                case Direction.North:
                    return "N";
                case Direction.East:
                    return "E";
                case Direction.South:
                    return "S";
                case Direction.West:
                    return "W";
                default:
                    return " ";
            }
        }

        // Changes the current direction based on input of the turn direction
        // Using char instead of string as instructions are all sent in one line with no spaces
        public void Turn(char turn_direction)
        {
            const int max_index = m_num_of_directions - 1;

            switch (turn_direction)
            {
                case 'L':
                    if (m_current_direction > 0)
                    {
                        m_current_direction--;
                    }
                    else
                    {
                        m_current_direction = (Direction)max_index;
                    }
                    break;
                case 'R':
                    if (m_current_direction < (Direction)max_index)
                    {
                        m_current_direction++;
                    }
                    else
                    {
                        m_current_direction = 0;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
