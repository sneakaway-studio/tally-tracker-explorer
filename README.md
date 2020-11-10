# TITLE?????

## Statement

A visualization depicting live|archived game data from [Tally Saves the Internet!](https://tallysavestheinternet.com) including player activity, achievements, and the trackers following them in real time.





## Instructions

Open the application and it will start automatically. Pressing the following keys will open / close various panels:

- **C**ontrol panel - Settings and controls
- **T**imeline panel - Shows the time of the event being played and size of the history and buffer
- **F**eed panel - For debugging only (adds high performance overhead)
- **ESC** / **Q**uit - Exit the application


#### Control panel options

- Settings - Set the resolution, fullscreen, and volume
- View FPS and resolution details
- Data - View the `status` and set the Mode (stop, set mode, then start):
	- `remoteLive` - Automatically refresh with live data from the server
	- `remoteArchive` - Fetch a live dataset once (using selected endpoint) and shuffle between buffer / history
	- `localArchive` - Same as `remoteArchive` except it uses the archived data in the project (endpoint is disabled)
- Timeline - View the `status`, `history` and `buffer` counts, and stop/start the application.






## About the data

The data included in this project comes from live player activity (people browsing the internet, and the marketing categories and web trackers following them):

- Player data includes
- Trackers





## Techrider



## Credits

Created by [Sneakaway Studio](https://sneakaway.studio) (Joelle Dietrick and Owen Mundy) during an eight-week artist residency funded by the [Andrew Mellon Foundation](https://mellon.org/) through the [Immersive Scholar](https://www.immersivescholar.org/) program with the [NC State University Libraries](https://www.lib.ncsu.edu/).

- The project is in the collection of ______________
- participants


## Technology

- Visualization - Unity 2020.1.2f, Universal Render Pipeline (URP)
- Game API - Node/Express
- [More about our process](PROCESS.md)
