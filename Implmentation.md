# Implementation

## How I started coding or how was the project made (Ideation Process)

So first I initiated the github repo now for the start I am thinking about making the core of the game which is first the player movement
Or the protagonist movement first after that we will move onto the Platform or the pulpit part

### DOOFUS (PROTAGONIST) BASICS

SO lets make a fresh scene called mainGame in which we will add a cube and a platform or a plane object in unity or the scene for the starter

Problems faced I decided to use the old input system so first from the project setting in the player I had to change in the options to both

Now after making a cube object and naming it as Doofus and then a 3d plane naming it as pulpit and giving a red material too the doofus
for the starter also after that I have made arranged all the file structure meaning naming scripts prefabs materials scenes.

After all this I made the player movement.cs and made a script file and used a namespace for better convention of writing code in Unity
namespace used here is called DoofusGame because it is a game based on our protagonist.

So using the Normal get Axis methods and the velocity from the timebeing I am not explaining the coding part after that
making the doofus object rigidbody and freezing its axis rotation and then putting this script on it and now volla our basic protagonist
moves

So after all this changes I commited My Code

### PULPIT Platform BASICS

So after making the initial setup in which my pulpit was a plane I understood It is not even possible to make a collision and give thickness for the same so because of this making pulpit a cube with 9x9 scaling as mentioned in the doc now after this I have made tags for the pulpit
such that it can be generated from the cs scripts or can be also found using the tag by GetGameObjectByTag option such that we can know what what is happening in the current scene

after putting the tags I also just added player tag to doofus also after that lets make the prefab for the pulpit which will be done by first giving it color and then making it a prefab and removing it from the hierarchy such that we can generate it using the cs scripts

after this I made two new scripts which one is added to the Empty Game Object at the first pos. in which I added the pulpitManager cs script which manages how to each pulpit will behave meaning inside which we have given helper functions for it and like spawn the next one find the adjacent position randomly and this is being caught by the pupil prefab script which we connect using this in the Manager so now the logicaly it is working now.

So the core main components it is working in which the platforms are forming adjacently and gets destroyed when for the time being given the half time of the other platform so it is half of the time of the other time destroyed to be precise for time being now.

the core logic of platform or pulpit is being done here now and the flow looks like this for the time being

Game Starts
    |
    v
PulpitManager creates FIRST Pulpit at (0,0,0)
    |
    V
Pulpit starts its timer (random 3-5 seconds)
    |
    V
After 50% of lifetime (e.g., 2 seconds if lifetime is 4s)
    |
    V
Pulpit calls: pulpitManager.SpawnNextPulpit()
    |
    V
PulpitManager creates SECOND Pulpit adjacent to first
    |
    V
First Pulpit eventually destroys itself
    |
    V
Second Pulpit (now halfway through life) spawns THIRD
    |
    V
REPEAT FOREVER...

So after all this changes I commited My Code

### Scoring Mechanism and Game Ending Logic

Now after this lets continue working on the logical part of the game meaning how does our doofus gets his score for that if he touches the pulpit it is one score and every new pulpit he touches his score counter gets incremented by one so.

So now what I have implemented is a new cs script called ScoreManager which does which is a singleton which means only instatiated once which will help us in making the score or changing it only once and on the other updates the various scoreManager instance doesnt get created then I have implenmented when the on the pulpit I have on the tag player when it OnCollisionEnter() the score increments by calling the increment function of the singleton instance of the scoreManager this is the basic logic and Also there is also another condition which checks if it is has the pulpit being stepped on any earlier time such that we dont count it twice.

For Game Ending logic we can do one thing is fallDetection.cs in which we see whether the doofus falls below -5 Y positions and then if it goes we will update in the ScoreManager and also check in the update section of the playerMovement in which we check whether the GameInstance is there or not if it is closed then we will stop the playerMovement and end the game.

so this is the core logic I have only loggeed the scoring process and the GameEnding logic in the debug console just for testing purpose.

### Camera Follow Basics

Like Every other game we will make the main camera follow the target our protagonist in this for this we will make the cameralFollow.cs in which we will get the compenent and target it using lookAt and we can achieve it very easily.

### UI BASICS

In this section I have implemented two canvas one for the screen or the main camera overlay which just shows the basic score ui in top of the main camera and using the score variable dynamicaly showing it in the top of the screen.

similiarily now I have added the canvas on the prefab of the pulpit such that the timer shows in the pulpit platform only I have imported the TEXTMESH pro for this two show the text in the UI and we have now two dynamically showing texts in the game UI one which shows score and the other which shows timer on the Pulpit Prefab.

### JSON LOADER ADDED

Now to finish the Level 1 and 2 of the assignment both I have made the JSON loader which takes in the values and for that I have made the GameSettings which initialises all the data structures for the JSON file to be loaded meaning it can be given the input and the GameSettingLoader which has the the methods which can will be called to get or update the data in the playerMovement file and the pulpit file accordingly so now we can change the values direclty through the JSON code which is more Convinant and also then it can also be an advantage while creating a menu system where the Users select the speed of our little protagonist.

### Made the UI panels for the new Start and Restart screen

It is the basic version of it and just shows two panels over the Start screen and the restart screen and the button changes the UI manager script which we created now which changes on the click and shifts from one scene to the other it is a very basic and logic nothing much fancy.



### Audio 

for basic engagement added FX sounds for score increment and gameOver and also added a RunnerMusic which I found from Unity Assets store as free


### Next Up is Animation 

In Animation now for only using the coding animations I have done which is normal fade in and fade out of the pulpit platform and the shaking which makes the game a bit more intense and engaging to play 




