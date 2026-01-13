
# GT Visualizer

Guitar string visualizer using WPF that reacts to live audio input.

**This project is mainly for learning purposes and is not yet finished.**

  

## Current Status

- **√** Audio capture from audio interface (NAudio)

- **√** Basic animation

- **√** Keyboard input for testing (keys `1`–`6` for individual strings, `Space` for all)

- **√** Frequency detection for individual guitar strings

  

## Future Plans

-   [ ] Tuner mode with UI
    
-   [ ] Synchronized lyrics display
    
-   [ ] Animations and effects
    
-   [ ] Support for different guitar tunings

## Tech Stack

- C# / WPF

- NAudio

- .NET 9.0

  

## How to Run

1. Clone the repo

```bash

git clone https://github.com/exessz/GT-Visualizer.git

2. Open in Visual Studio

3. Restore NuGet packages (NAudio)

5. Ensure your audio interface (e.g., Focusrite) is configured in the source code.

6. Run the project