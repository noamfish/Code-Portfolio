# Code-Portfolio
Created to display portions of my projects to friends or curious employers.
The current additions to this portfolio are parts of my current RTS project. 
I have chosen these as I believe they are some of my best examples of well written code and strong object encapsulation.

The folders *State Machine* and *Unit Scripts* contain everything to do with my handling of unit interactions in the game.  
Each unit has compositional components, which encapsulate their different functions, but work together using a statemachine to simplify capabilities at a given time. 

The folder *Inventory* contains all of the mechanisms by which I create an inventory, store items, and update UI visuals.  
The inventory is a scriptable object that contains an array of inventory slots and is capable of checking its contents and increasing and decreasing its quantities.  
The inventory manager is tasked with maintaining the inventory UI. 
