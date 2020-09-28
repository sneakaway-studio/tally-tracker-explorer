# tally-ncsu-viz


To do
- [x] Setup
  - [x] Create repo
  - [x] Create Unity project (2020.1.2f1)
- [ ] Data
  - [x] Update API with basic feed data
  - [x] Get Feed data in Unity
  - [x] Convert Feed data to JSON object
  - [x] Create EventManager
  - [x] Update API with detailed feed data
    - [x] Change format of feeds table to use `JSON_OBJECT()`
    - [x] Update feeds on website to use `eventData` object from each row
    - [x] Update API to use `eventData` object from each row
  - [ ] Build Feed data testing / monitor UI
- [ ] Timeline
  - [x] Time UI class
    - [ ] Button: Change refresh time
    - [ ] Button: playback restart
    - [ ] Scrubber: playback speed
  - [x] Create playback 
    - [x] Coroutine to play event at specific time
- [ ] Visuals -> Monsters
  - [x] Create addressables system for sprite animation slicing workflow
  - [x] Create MonsterPool
   - [ ] Create add / subtract from pool methods
- [ ] Visuals -> Players
  - [ ] Initialize
    - [x] Use Feed data to build GameObjects and display in "Universe"
    - [ ] Ensure players aren't added twice with new feed data
  - [ ] Player -> Actions 👈 
    - [ ] Create a series of actions (physics controlled from methods) that visualize different event types on playback
      - [ ] Stream
        - [ ] Click 
          - Player movement: velocity and Y position increases, random X direction
          - Sound: ping
          - Animation: expanding concentric rings similar to "[radar](https://www.provideocoalition.com/wp-content/uploads/Radar.gif)" effect but with better colors
        - [ ] Like
          - Player movement: ?
          - Sound: ?
          - Animation: ?
      - [ ] Attack
        - [ ] Awarded
          - Player movement: ?
          - Sound: ?
          - Animation: ?
      - [ ] Badge
        - [ ] Awarded (changes depending level)
          - Player movement: ?
          - Sound: ?
          - Animation: ?
      - [ ] Consumable
        - [ ] Found (changes depending type, stat)
          - Player movement: ?
          - Sound: ?
          - Animation: ?
      - [ ] Disguise
        - [ ] Awarded
          - Player movement: Opacity Shake, CSShake
          - Sound: Spell/magic sound like https://freesound.org/people/suntemple/sounds/241809/
          - Animation: Concentric triangles like player passes through a prism
      - [ ] Tracker
        - [ ] Blocked
          - Player movement: ?
          - Sound: ?
          - Animation: ? 
      - [ ] Battle
        - [ ] In-progress
          - Player movement: ?
          - Sound: Light battle music (on zoomed in)
          - Animations: Rumble animation appears over player
        - [ ] Attack
          - Player movement: ?
          - Sound: ?
          - Animation: Attack animation GIF
        - [ ] Win 
          - Player movement: ?
          - Sound: ?
          - Animation: Show win screen from game
        - [ ] Lost
          - Player movement: ?
          - Sound: ?
          - Animation: ? 
      - [ ] Leaderboard
        - [ ] Position in leaderboard changes
          - Player movement: ?
          - Sound: ?
          - Animation: ?   
  - [ ] User -> Movement 👈 
    - [x] Create user (physics controlled) floating movement (Jellyfish?)
  - [ ] User -> Trails 👈 
    - [ ] Create "Nyan Cat" trails (particle system?) (some examples on [google](https://www.google.com/search?q=unity+trail+renderer&safe=off&rlz=1C5CHFA_enUS903US909&sxsrf=ALeKk038imz2qRqefBNgel1Fi7zgS7CyHw:1600720422081&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjo95GhjPvrAhUFqlkKHQFpAAQQ_AUoAnoECAwQBA&biw=1239&bih=766))
      - [ ] Each trail is a product marketing category from streams
      - [ ] [Colors](https://github.com/sneakaway-studio/tally-api/blob/master/public/assets/css/sass/custom.scss)
      - [ ] Add/remove trails based on streams updates
      - [ ] Add/remove monsters from data trail based on streams updates
- [x] Visuals -> Anaglyph3D
  - [x] Add / test Anaglyph3D shader
- [ ] Visuals -> Environment
  - [ ] Particles 👈
    - [ ] Use particle system to create small floating objects to give the visual display depth
      - [ ] Snow similar to the [upside down](https://www.youtube.com/watch?v=LwmnNzY7gdo&ab_channel=AmbientWorlds) floaty bits
      - [ ] Detritus in undersea life a.k.a "[marine snow](https://oceanservice.noaa.gov/facts/marinesnow.html)"
      - [ ] [Stars in cosmos](https://penningdownheart.files.wordpress.com/2018/03/stars-3000x2000-purple-cosmos-hd-7172.jpg)
  - [ ] Lighting 👈
    - [x] Point lights on GameObjects
    - [ ] Light emitters on user trails
    - [ ] Environmental lighting
    - [ ] Changes to lighting depending on time of day
    - [ ] Baking, etc. performance considerations
    - [ ] Fog examples [1](https://forum.unity.com/threads/how-can-i-control-fog-color-based-on-skybox-color.311706/), [2](https://carlburton.itch.io/islands), [3](https://magazine.renderosity.com/article/5204/taking-a-look-at-unity-fog)
  - [ ] Background
- [ ] Test in space
  - [ ] 8 cameras
  - [ ] Figure out user control device



### Notes on the setup of this Unity project



- [How to get Good Graphics in Unity](https://www.youtube.com/watch?v=owZneI02YOU&ab_channel=Brackeys) (8:13)
- [REALTIME LIGHTING in Unity](https://www.youtube.com/watch?v=wwm98VdzD8s&ab_channel=Brackeys) (15:47)



