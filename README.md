# ProgrammingTest

Player1 Controls:
UP: 	W
DOWN:	S
LEFT:	A
RIGHT:	D
ACTION:	E

Player2 Controls:
UP:	KeyCode.Keypad8;
DOWN:	KeyCode.Keypad5;
LEFT:	KeyCode.Keypad4;
RIGHT:	KeyCode.Keypad6;
ACTION:	KeyCode.Keypad7;

Action Keys:
Action keys can be used in three locations:
Trash - throws away combination plates
Cuttingboard - Picks up combination plates
Plates - places vegetables on plate

Issues:
I was having a few issues with how I would display inventory and the plates.  I was deciding between images and text and ended up using UI to help display everything.  Some other issues I ran into and managed to resolve were comparing the ingredients to the customers request and having the dish submission be incorrect if there were too many ingredients.  

Overall Commentary:
Initially, I was taking some time to figure out how I would develop the game.  I was breaking down the game as much as I can in terms of how I would be coding it.

Throughout the development process, I ran into a few issues.  I was having trouble mimicing the movement of GetAxis from Unity since that allowed the movement to feel fluid.  I managed to get it close to it, but there's an issue when you press left then right, or up then down.  You end up fighting the "momentum" of your character, but for the most part, I'm happy with the result. 