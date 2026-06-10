# Scary Shawarma Nigh Shift - Horror Game
Based off the popular Roblox game "Scary Shawarma Horror".

## Features
* VHS Analog Horror Style Tutorial.
* Different anomalies.
* Night Vision Camera to detect anomalies.
* Music / Sound Effects! - NOTE: Sound only works on Windows. System.Media is used for Audio.
* Satisfaction Bar to track progress - Game Over if bar drops to zero... or if you die.

## How to Run
1. Download files
2. Open terminal / command prompt.
3. Run the command "dotnet run".

## How to Play
* Make sure to serve normal customers
* Deal with anomalies accordingly
* Enter C for cameras
* You will have 4 options to choose from when dealing with customers. Only one is correct - the rest have consequences.

## Coding the Game (Notes for Coders)
* TypeWriter text effect: TypeWriter();
* For Sounds (Name is placeholder for variable name):
    * Play: Name?.Play(); OR Name?.PlayLooping();
    * Stop: Name?.Stop();
* Delays: Thread.Sleep(milliseconds);
