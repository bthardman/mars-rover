Brad Hardman - Mars Rover Challenge

Written in C#, can be compiled in Visual Studio or on the command line with the following command and then calling MarsRover:
c:\windows\Microsoft.NET\Framework\v3.5\csc.exe /t:exe /out:MarsRover.exe c:\Users\bradl\Source\Repos\mars-rover\MarsRover\MarsRoverMission.cs  c:\Users\bradl\Source\Repos\mars-rover\MarsRover\Map.cs c:\Users\bradl\Source\Repos\mars-rover\MarsRover\Vehicle.cs c:\Users\bradl\Source\Repos\mars-rover\MarsRover\TurningCircle.cs

My approach to this challenge was to showcase the type of code I prefer to write: easily readable, extendable, robust and maintainable code.
To do this I structured the code in an object oriented approach, always considering how a future developer could extend on the classes I wrote.

Using encapsulation I made the member data private for classes: Map, Vehicle and Turning Circle. 
The values of this data is then changed inside a public function which have strict validation using checks on the input and switch statements.
By utiziling this method it reduces the amount of bugs and stops other developers from readily accessing this data.

Through using enums, switch statements and const ints to define the amount of directions in the Turning Circle class it makes it clear to future developers on where to extend the codebase.
Using enums increases readibility of the code and avoids use of integers for array indexes, which reduces potential bugs that can occur through future development.
Additionally by defining a const int for the amount of directions a developer only has to increment that and the max index will then be updated.

The Turning Circle class would be the most clear class to extend: being able to add NW, SW, SE, NE or any other direction. 
The generic functions such as Turn and MoveForward that do not rely on indexes will continue to work as expected, while only switch statements need extending for GetModLocation() and GetDirectionIdentifier().
Additionally the instruction set could be easily expanded by adding new instructions for the rover in the switch statement in TakeInstructions function and adding a function to implement them.

The code was tested with the included unit test but also has command line input to type or paste in a file path and the output will be returned back.