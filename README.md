All functions from assiggnment requirements are fully functional, and should score a 9 or 9.5 /10.
The remaining mark is absent because the victory conditions for circle are not implemented, but corners and sides are. (Victory message is displayed in the console, all other feedback is represented on the game board.)

If this is not sufficient to run the assignment, please email me at Zayuz@hotmail.ca

**DISCLAIMER: I attempted to upload the full assignment in every way provided by the professor, but the compressed unity project file was 500MB and thus too large to be added anywhere. I recognized that the only part that I had changed to complete the project from a basic unity project was the assets, especially the tile and board scripts that perform all of the required work for the system. Even the prefabs should be included in this area. Thus, they are uploaded alone to avoid this ridiculous project size. I cannot figure out a way to otherwise submit this assignment, but would be happy to adjust the submission if a method is provided.**

Created entirely by Ryan Peckham.

CHECKLIST:
1. [ 4/4 ] Scene setup. The game is modeled with a 3D scene where the board is composed of
individual hexagonal tiles. The board is created at run time with an algorithm that places the tiles
on a surface. The board has an hexagonal shape with 10 hexahedra along each side. The scene
has illumination so that the board can be seen while playing the game.
2. [ 1/1 ] Materials/texturing/colors. The tiles are textured/colored in at least three different
ways so that we can understand what is going on in the game. For example, when a player points
the mouse at a tile, the tile changes color, and after a player selects a valid tile, the color of the
tile changes to the player’s color.
3. [ 1/1 ] User interaction and visual feedback. The player can select a tile with the mouse, and
a minimum of visual feedback is given to the user so that the player knows which tile is being
selected and what was the outcome of the selection, that is, whether the selection was a valid
move or not.
4. [ 3/4 ] Game mechanics.
4.1. [ 0.5/0.5 ] The game ensures that a player cannot select a tile that has already been selected by
the other player.
4.2. [ 0.5/0.5 ] After one player made a move, the turn goes to the other player.
4.3. [ 2.0/3.0 ] The mechanic for checking the winning condition is fully implemented: when a valid tile
has been selected, the game checks if the addition of the tile forms one of the three winning
structures. If a winning structure was formed, the game indicates which color (player) has won
