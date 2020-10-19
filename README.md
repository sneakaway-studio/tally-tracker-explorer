# tally-ncsu-viz



## Notes

- Unity 2020.1, URP



## To do items




### Setup

- [x] Create repo
- [x] Create Unity project (2020.1.2f1)
- [x] [Git, Git LFS, SmartMerge](https://github.com/omundy/dig250-game-art-dev/blob/master/reference-sheets/Unity-Git.md)
- [ ] Choose a name!





### Data & Timeline

- [x] Data
	- [x] Update API with basic feed data
	- [x] Get Feed data in Unity
	- [x] Convert Feed data to JSON object
	- [x] Create EventManager
	- [x] Update API with detailed feed data
		- [x] Change format of feeds table to use `JSON_OBJECT()`
		- [x] Update feeds on website to use `eventData` object from each row
		- [x] Update API to use `eventData` object from each row
- [x] Timeline
	- [x] Timeline class
	- [x] Create playback
	- [x] Coroutine to play event at specific time




### Playback History and Buffer automation

- [ ] Create code that
	- [ ] Automatically gets, or handles a lack of, new events
	- [ ] Can consume data from the live API or local `JSON` files without changing anything
	- [ ] Potentially do it like:
		- [ ] Use two C# collections (either a List or Dictionary) named `buffer` and a `history` that can be sorted by a date string.
		- [ ] As the events are visualized, the event object is moved from the `buffer` to the `history`.
		- [ ] If no (or not enough) new events are found on the next API call, then a "rewind" can happen where a chunk of events from history are placed back in the buffer and the "playhead" can restart at the end of the buffer (until the next check).


### Timeline visualization

- [ ] Build timeline Visualization that shows `history` and `buffer` 👈
	- [ ] "Playhead" that moves horizontally with the current event
	- [ ] `History` and `buffer` are both visualized, with dots showing number in each collection, on either side
	- [ ] Playhead moves right as new events happen, increasing the `history` and decreasing the size of the `buffer`
	- [ ] As `buffer` is filled back up `history` is deleted and playhead resets to left.
	- [ ] Use [Colors](https://github.com/sneakaway-studio/tally-api/blob/master/public/assets/css/sass/custom.scss) from the website palette in the interface





### Interaction

- [ ] Add methods so viewer with a controller (keyboard, joystick, etc.) in the Visualization Studio **OR** someone using this with an iPad can: 👈
	- [ ] Use horiz/vert axis (controller) or pinch/zoom (tablet) to select different players
	- [ ] Click / press button / touch to zoom camera into and follow player
	- [ ] While zoomed-in, display additional data on that player (from feed, username, etc.)
	- [ ] Use horiz/vert axis (controller) or swipe (tablet) to shift to different player
	- [ ] Either after a period of no activity automatically, or with player click / press button to, zoom back out  


### Testing

- [ ] Build Feed data testing / monitor UI
	- [ ] Dropdown to switch between live / local
	- [x] Button: playback restart




### Monsters

- [x] Sprite animation slicing
- [x] Add monster sprites to follow players 👈


### Players

- [ ] Initialize
	- [x] Use Feed data to build GameObjects and display in "Universe"
	- [ ] Ensure players aren't added twice with new feed data
- [x] Movement
	- [x] Create player (physics controlled) floating movement (Jellyfish?)
- [ ] Actions (controlled from code) that visualize different event types on playback
	- [x] 👆 Stream - Click ✅
		- Player movement: `Pop_Shake_md.anim`
		- Sound: [Click.ogg](Assets/_Project/Sounds/Effects/Click.ogg)
		- Extra effects: expanding concentric rings similar to "[radar](https://www.provideocoalition.com/wp-content/uploads/Radar.gif)" effect but with better colors, maybe toned down color
	- [ ] 👍 Stream - Like
		- Player movement: Pulses bigger then glows, similar to "[light bulb](https://dribbble.com/shots/11115983-Creative-Block)" effect
		- Sound: [Like.ogg](Assets/_Project/Sounds/Effects/Like.ogg)
		- Extra effects: hearts particle system like trailer?
	- [ ] 🧨 Attack - awarded
		- Player movement: ?
		- Sound: [Attack.ogg](Assets/_Project/Sounds/Effects/Attack.ogg)
		- Extra effects: ?
	- [ ] 🏆 Badge - awarded (changes depending level)
		- Player movement: accelerates right along the x-axis or concentric circles emanating from player’s icon
		- Sound: [Badge.ogg](Assets/_Project/Sounds/Effects/Badge.ogg)
		- Extra effects: badge animation, drawn like "[this icon](https://dribbble.com/shots/5499453-Elevate)", use Miguel's icons in leaderboard's feed
	- [ ] 🍪 Consumable - found (changes depending type, stat)
		- Player movement: accelerates right along the x-axis or concentric circles emanating from player’s icon
		- Sound: [Consumable.ogg](Assets/_Project/Sounds/Effects/Consumable.ogg)
		- Extra effects: consumable animation, drawn like "[this icon](https://dribbble.com/shots/5499453-Elevate)", use Miguel's icons in leaderboard's feed
	- [ ] 😎 Disguise - awarded
		- Player movement: Opacity Shake, CSShake
		- Sound: [Disguise.ogg](Assets/_Project/Sounds/Effects/Disguise.ogg) **OR** Spell/magic sound like https://freesound.org/people/suntemple/sounds/241809/
		- Extra effects: Concentric triangles like player passes through a prism OR disquise animation, drawn like "[this icon](https://dribbble.com/shots/5499453-Elevate)", use Miguel's icons in leaderboard's feed
	- [ ] 🕷️ Tracker - blocked
		- Player movement: ?
		- Sound: [Tracker.ogg](Assets/_Project/Sounds/Effects/Tracker.ogg)
		- Extra effects: tracker animation, drawn like "[this icon](https://dribbble.com/shots/5499453-Elevate)", use Miguel's icons in leaderboard's feed
	- [ ] 💥 Battle - In-progress
		- Player movement: "rumble" CSShake little shake
		- Sound: [Battle-In-Progress.ogg](Assets/_Project/Sounds/Effects/Battle-In-Progress.ogg) **OR** Light battle music (on zoomed in)
		- Extra effects: Rumble animation appears over player (dust clouds or too much?)
	- [ ] 💥 Battle - Win
		- Player movement: does a celebratory flip
		- Sound: [Battle-Win.ogg](Assets/_Project/Sounds/Effects/Battle-Win.ogg)
		- Extra effects: Show win screen from game OR tracker animation, drawn like "[this icon](https://dribbble.com/shots/5499453-Elevate)", use Miguel's icons in leaderboard's feed
	- [ ] 💥 Battle - Lost
		- Player movement: Y-value increases +50 px (down on screen)
		- Sound: [Battle-Lost.ogg](Assets/_Project/Sounds/Effects/Battle-Lost.ogg)
		- Extra effects: Goes grey or loses opacity
	- [ ] 🔢 Leaderboard position changes
		- Player movement: Higher in leaderboard —> longer tail
		- Sound: [Leaderboard.ogg](Assets/_Project/Sounds/Effects/Leaderboard.ogg)
		- Extra effects: Long tail inspiration: https://dribbble.com/shots/11776498-Dachshund-Skater


Other ideas on movement:
- CSShake hard shake
- Rumble glitch GIF, see "[this pigeon](https://dribbble.com/shots/10793942-Pigeon-animation-logo)"




### Effects

- [x] PlayerTrails 👈
	- [x] Create "Nyan Cat" trails (particle system?) (some examples on [google](https://www.google.com/search?q=unity+trail+renderer&safe=off&rlz=1C5CHFA_enUS903US909&sxsrf=ALeKk038imz2qRqefBNgel1Fi7zgS7CyHw:1600720422081&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjo95GhjPvrAhUFqlkKHQFpAAQQ_AUoAnoECAwQBA&biw=1239&bih=766))
	- [ ] Connect each trail to a product marketing category from streams using colors from the monster gradients
	- [ ] Add/remove trails based on streams updates
	- [ ] Add/remove monsters from data trail based on streams updates
- [x] Anaglyph3D
	- [x] Add / test Anaglyph3D shader
- [x] Marine Snow / Floating stars 👈
	- [x] Use particle system to create small floating objects to give the visual display depth, for example:
		- [x] Snow similar to the [upside down](https://www.youtube.com/watch?v=LwmnNzY7gdo&ab_channel=AmbientWorlds) floaty bits
		- [x] Detritus in undersea life a.k.a "[marine snow](https://oceanservice.noaa.gov/facts/marinesnow.html)"
		- [x] [Stars in cosmos](https://penningdownheart.files.wordpress.com/2018/03/stars-3000x2000-purple-cosmos-hd-7172.jpg)



### Lighting

- [x] Change project to URP (Universal Render Pipeline)
- [x] Setup [2D renderer and lights](https://www.youtube.com/watch?v=nkgGyO9VG54&t=53s&ab_channel=Brackeys)
- [x] Point lights on GameObjects
- [ ] Light emitters on player trails
- [ ] Environmental lighting
- [ ] Changes to lighting depending on time of day
- [ ] Baking, etc. performance considerations
- [ ] Add Fog
	- [ ] Examples [1](https://forum.unity.com/threads/how-can-i-control-fog-color-based-on-skybox-color.311706/), [2](https://carlburton.itch.io/islands), [3](https://magazine.renderosity.com/article/5204/taking-a-look-at-unity-fog)
- [ ] Add texture to background


## Delivery

- [ ] NCSU Visualization Studio
	- [ ] Implement 8 camera system
	- [ ] Figure out player control device
- [ ] Mobile app?




### Notes on the setup of this Unity project

- [How to get Good Graphics in Unity](https://www.youtube.com/watch?v=owZneI02YOU&ab_channel=Brackeys) (8:13)
- [REALTIME LIGHTING in Unity](https://www.youtube.com/watch?v=wwm98VdzD8s&ab_channel=Brackeys) (15:47)



## Documentation / Instructions

### Testing

- You can change the type of data to see either all data (mainly streams, but also game data) or feed (only game objects, which is more diverse). Select DataManager and change “Chosen endpoint” to “Range plus stream 1 hour” or “Recent 20” (this is the more diverse game objects).
