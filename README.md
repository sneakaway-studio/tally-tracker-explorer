
# Tally Tracker Explorer


- [About](#About)
- [Instructions](#Instructions)
- [Credits](#Credits)



### About

A visualization depicting live and archived game data from [Tally Saves the Internet!](https://tallysavestheinternet.com) including player activity, achievements, and the trackers following them in real time.

> Tally Saves the Internet is a browser extension that transforms the data that advertisers collect into a multiplayer game. Once the browser extension is installed, a friendly pink blob named Tally lives in the corner of your screen and warns you when companies translate your human experiences into free behavioral data. When Tally encounters “product monsters” (online trackers and their corresponding product marketing categories) you can capture them in a turn-based battle (e.g. “Pokémon style”) transforming the game into a progressive tracker blocker, where you earn the right to be let alone through this playful experience.


![tally tracker explorer](_Documentation/tally-explorer-combined-800w.png)





Developed during COVID when people are increasingly online, Tally Tracker Explorer captures live data from Tally Saves the Internet and displays this data as a sea of avatars, each surrounded by small product monsters and attached to their unique data trails, colored to match their top marketing categories.

Both Tally Saves the Internet and Tally Tracker Explorer’s core goal is to visualize the internet and its trackers in a way that makes the invisible (trackers) visible and the lingo familiar. As Harvard Professor Shoshana Zuboff, if you can name the parts of a system, you can understand it. If you can understand it, you can push for change.

Sure it’s beautiful, but what does Tally Tracker Explorer tell me about anything? Tally Saves the Internet gives you much more accurate insights into how advertisers see you and gives you reminders about data tracking in the form of playful product monsters while you block trackers. What Tally Tracker Explorer does offer is a light-hearted visualization and collective spirit during this tough year of isolation, in and out of quarantine.

During COVID-19 quarantines, when we spend more time online, Tally Saves the Internet offers a way to reveal and re-envision the internet’s invisible structures as productive spaces for artistic interventions. As Klein and D’Agnozio describe in their 2020 book Data Feminism from MIT Press, if data is the new oil, those people who profit from this resource are thrilled while the rest of us range from indifferent to terrified. Tally offers its audience an alternative: it transforms advertising data into a multiplayer game that elevates emotion and alternative forms of embodiment as a way to examine power.
NC State’s Immersive Scholar Project Page, https://tallytrackerexplorer.immersivescholar.org
Tally Tracker Explorer on the App Store and https://itch.io/







## Instructions


### Running the application

Open the application and it will start automatically. Pressing the following keys will open / close various panels:

- **C**ontrol panel - Settings and controls
- **T**imeline panel - Shows the time of the event being played and size of the history and buffer
- **F**eed panel - For debugging only (adds high performance overhead)
- **ESC** / **Q**uit - Exit the application


### Zoom-to-player instructions

operation | keyboard | gamepad  
--- | :-------------: | ---
zoom in | ↑ | left joystick ↑
select players | ← or → | left joystick ← or →  
zoom out | ↓ | left joystick ↓  


### Control panel options

- Settings - Set the resolution, fullscreen, and volume
- View FPS and resolution details
- Data - View the `status` and set the Mode (stop, set mode, then start):
	- ~~`remoteLive` - Automatically refresh with live data from the server~~
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

Commissioned by NC State’s Immersive Scholar project,

- The project is in the collection of ______________
- participants


## Technology

- Visualization - Unity 2020.1.2f, Universal Render Pipeline (URP)
- Game API - Node/Express
- [More about our process](PROCESS.md)
