using System.Drawing;

namespace MarsRover
{
    public class Map
    {
        // Member Data
        private Point m_max_size_of_map;

        // Functions
        public Map(Point size_of_map)
        {
            m_max_size_of_map = size_of_map;
        }

        // Validate that a position is not off the map
        // Can be extended to add obstacles
        public bool ValidatePosition(Point point_to_check)
        {
            return (0 <= point_to_check.X && point_to_check.X <= m_max_size_of_map.X &&
                    0 <= point_to_check.Y && point_to_check.Y <= m_max_size_of_map.Y);
        }
    }
}
