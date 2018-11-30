using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace MarsRover
{
    public class MarsRoverMission
    {
        // Member Data
        private const string m_map_pattern = "\\d+\\s\\d+";
        private const string m_vehicle_pattern = "\\d+\\s\\d+\\s\\w+";
        private const string m_instruction_pattern = "\\w+";
        private const int m_num_instructions_map = 1;
        private const int m_num_instructions_vehicle = 2;

        // Using List<string> as data can be added pushed on dynamically
        private List<string> m_output;

        // Functions

        public MarsRoverMission()
        {
            m_output = new List<string>();
        }

        // Main for controlling user input of file path
        public static void Main(string[] args)
        {
            // Initialise mission
            MarsRoverMission mission = new MarsRoverMission();

            Console.Write("Input data (.txt) file path: ");
            string file_path = Console.ReadLine();
            try
            {
                // Read in instructions from file
                string[] instructions = File.ReadAllLines(file_path);
                string[] output = mission.DoInstructions(instructions);
                foreach (string output_str in output)
                {
                    Console.WriteLine(output_str);
                }
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading file: \"" + file_path + "\"");
            }
        }

        // Takes in array of instructions to validate and complete
        public string[] DoInstructions(string[] instructions)
        {
            if (ValidateInstructions(instructions))
            {
                // Initialise Plateu
                string[] map_coords = instructions[0].Split(' ');
                Point map_size = new Point(Convert.ToInt32(map_coords[0]), Convert.ToInt32(map_coords[1]));
                Map plateu = new Map(map_size);

                bool successful_move = true;
                int num_of_rovers = (instructions.Count() - m_num_instructions_map) / m_num_instructions_vehicle;
                Vehicle[] rovers = new Vehicle[num_of_rovers];

                int rover_count = 0;

                // Loop through remaining vehicle definitions and instructions
                for (int instruct = m_num_instructions_map; instruct < instructions.Count(); instruct = instruct + m_num_instructions_vehicle)
                {
                    // Convert input string into information
                    string[] vehicle_definition = instructions[instruct].Split(' ');
                    Point vehicle_location = new Point(Convert.ToInt32(vehicle_definition[0]), Convert.ToInt32(vehicle_definition[1]));
                    string direction = vehicle_definition[2];

                    rovers[rover_count] = new Vehicle(vehicle_location, direction, plateu);

                    // validate instructions
                    for (int char_index =0; char_index < instructions[instruct + 1].Count(); char_index++)
                    {
                        char movement_to_do = instructions[instruct + 1].ElementAt(char_index);

                        // Do movement with rover
                        successful_move = rovers[rover_count].TakeInstructions(movement_to_do);
                        if (successful_move == false)
                        {
                            // If move failed add error message and return
                            m_output.Add("The " + char_index + " instruction caused the rover to go off the map, not continuing instructions");
                            break;
                        }           
                    }

                    // Will only output rover positional data if the previous moves were successful
                    // Otherwise an error message has already been added above
                    if (successful_move)
                    {
                        m_output.Add(rovers[rover_count].GetPositionalDetails());
                    }

                    rover_count++;
                } 
            }

            return m_output.ToArray();
        }

        // Validates list of instructions by matching them against regular expressions
        private bool ValidateInstructions(string[] instructions)
        {
            bool validated = true;

            string map_string, vehicle_string, instruction_string;

            // Amount of instructions for: Count % vehicle != map % vehicle
            bool incorrect_num_of_instruct = (instructions.Count() % m_num_instructions_vehicle != m_num_instructions_map % m_num_instructions_vehicle);

            // Validate amount of instructions 
            if (instructions.Count() == 0 || incorrect_num_of_instruct)
            {
                validated = false;
                m_output.Add("Invalid list of instructions - must be have " + m_num_instructions_map + " number of Map instructions and " + m_num_instructions_vehicle + " number of Vehicle instructions");
            }

            // Validate map string
            map_string = instructions[0];
            if (Regex.IsMatch(map_string, m_map_pattern) == false)
            {
                validated = false;
                m_output.Add("Map size did not match the required format");
            }

            // Loop through remaining vehicle definitions and instructions
            for (int i = m_num_instructions_map; i < instructions.Count(); i = i + m_num_instructions_vehicle)
            {
                // Validate vehicle definition
                vehicle_string = instructions[i];
                if (Regex.IsMatch(vehicle_string, m_vehicle_pattern) == false)
                {
                    validated = false;
                    m_output.Add("Vehicle initial location and direction did not match the required format");
                }

                // validate instructions
                instruction_string = instructions[i + 1];
                if (Regex.IsMatch(instruction_string, m_instruction_pattern) == false)
                {
                    validated = false;
                    m_output.Add("Map size did not match the required format");
                }
            }

            return validated;
        }

    }
}