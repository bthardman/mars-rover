using System.Drawing;

namespace MarsRover
{
    public class Vehicle
    {
        // Member Data
        private Point m_location;
        private TurningCircle m_steering_wheel;
        private Map m_current_map;

        // Functions
        public Vehicle(Point location, string initial_direction, Map current_map)
        {
            m_location = location;
            m_steering_wheel = new TurningCircle(initial_direction);
            m_current_map = current_map;
        }

        // Take instructions on how to control the vehicle
        public bool TakeInstructions(char instruction)
        {
            switch (instruction)
            {
                case 'L':
                case 'R':
                    if (m_steering_wheel == null)
                        return false;
                    m_steering_wheel.Turn(instruction);
                    break;
                case 'M':
                    return MoveForward();
            }
            return true;
        }

        // Moves the vehicle forward using the direction it is facing
        private bool MoveForward()
        {
            bool successful_move = true; ;
            Point modify_location = m_steering_wheel.GetModLocation();
            Point new_location = new Point (m_location.X + modify_location.X, m_location.Y + modify_location.Y);
            // Validate that the vehicle will not pass off the map by completing this move
            if (m_current_map.ValidatePosition(new_location))
            {
                m_location = new_location;
            }
            else
            {
                // Throw error for invalid move
                successful_move = false;
            }
            return successful_move;
        }

        // Getter for output
        public string GetPositionalDetails()
        {
            return (m_location.X.ToString() + " " + m_location.Y.ToString() + " " + m_steering_wheel.GetDirectionIdentifier());
        }
    }
}
