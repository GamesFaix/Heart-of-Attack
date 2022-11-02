STARTING A GAME:

-Add players (2-8)

-Select faction for each player (2 players cannot have the same faction).
-Game cannot be started until all players have a faction.
-'Force random' button assigns a random untaken faction to each undecided player.

*BUG: At least one player must have their faction randomly forced or the game will not start.

-Select board size (2x2 to 20x20).

-Click 'Start game'

-Each player starts with one 'Attack King' token in a random cell on the board.
-Starting turn order is randomly chosen.

*BUG: Small map sizes (less than 6x6) may result in some players Attack Kings not having room to spawn. For best results, map size should be 1 greater than number of players.

/////////////////////

GAME GUI:

-The GUI is divided into 4 main areas: 'Board', 'Inspector', 'Queue', and 'Log'.

-Board:
--The Board is the largest section and will scale with window/screen size.
--Zoom control is located on top of the board.  If zoomed in, you can adjust the current view with scroll bars on the right or bottom.
--Right-click any Token on the Board to inspect it in the Inspector (right of screen)
--Shift+right-click any Cell to inspect it.
--Any time a Token or Cell is a legal target, it will be highlighted yellow, and can be selected by a left-click.

-Queue:
--The Queue is found in the bottom left corner and displays the turn order.  The Token whose turn it currently is will be on top, the Token that just took a turn will be on bottom.  You may have to scroll down to see all Tokens.
--Right-clicking on a Token's name in the Queue will inspect them.
--Left-clicking on a Token's name in the Queue will highlight them on the Board.
--Some Token stats are displayed next to Token names in the Queue, but full stats are only available in the Inspector.

-Log:
--The Log is not very important
--The Log prints out each event that happens in the game.
-*BUG: Not all events will log and some will not log properly.
-The text field under the Log will be usable in a future version.

-Inspector:
--The Inspector takes up the right portion of the screen.
--Most icons in the Inspector have tooltips that can be viewed by hovering the mouse over them and holding 'shift'.
--Above the Inspector are three buttons: 'Game', 'Manual', and 'Quit'.
---'Game' displays the normal in-game Inspector.
---'Manual' displays controls for manually overriding different aspects of the game to fix bugs or use features that have not been automated yet.
---'Quit' returns to the main menu.


//////

GAMEPLAY:
-A Token is any object on the board, the two main types are Units and Obstacles.
-Each player starts with an Attack King Unit on the board. 
-If your Attack King dies, you lose. (See tooltip on 'Crown' icon)
-Players can control any Units created by their Attack King, or created by any Units created by their Attack King.

Space:
-Cells may contain more than one token (See tooltip on 'cube' icon)

Time:
-Turns are given to each Unit, not to each player.  (See tooltip on 'clock' icon)

Money:
-Units have two resources (Energy and Focus) that they may use to perform Actions on their turn. Units get Energy at the start of their turn and lose all Energy at the end of their turn.  Focus can be stored over time.  (see tooltip on 'lightning' and 'book' icons)

Actions:
-A Unit's Actions will be displayed when the are inspected.  Mouse-over Action buttons for more details. (Cost, aim, explanation...) (Some icons in action details have tooltips, but not all have been created yet)
-Each Action can only be used once per turn.
