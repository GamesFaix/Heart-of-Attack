# Fantasy Battle Cardboard

Fantasy Battle Cardboard was a tabletop version of Fantasy Battle Deluxe, created to test game mechanics while we were learning how to use Unity3D. The board was a piece of posterboard with a 3in. x 3in. grid (lost). Game tokens were printed on 3in. x 3in. cardstock. An "assistant" app was created in Unity to keep track of the turn order, and this evolved into the game engine over time.

Based on memory and the dates on archive files, I think this stage was worked on from around August 2011 to November 2011.

## Tools

* Open Office Calc, Excel, or Google Sheets for docs
* [Unity3D 3.4.x](https://download.unity3d.com/download_unity/UnitySetup-3.4.2.exe) for running source files
* Photoshop or Gimp for graphics

## Contents

* `/Cards` contains graphics files for printing the game tokens.
* `/Documentation` contains some design documents.
* `/Queue` contains Unity project files and exports for the queue app.

## Highlights

* Queue v3 is a pretty stable and easy to use early version. You can type in unit names and initiative values, and add them to the queue.
* Queue v7 is a lot less intuitive, but more advanced. The screen is divided into three panes: left is the queue of units, middle is an inspector that shows the details of the last unit you clicked on, right is a command prompt. Try `create katandroid` and `kill katandroid a` in the text box.
