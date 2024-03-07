# Evaluation

Overall, I think this project is a solid foundation for a Match 3 game, 
with good feature for finding matches and collapsing. There are a lot of 
performance issues, and have a sizable coupling issues that can harm the 
potential of scaling

# Advantage
- Seperation between data and implementation of the code, in the form of GameSettings. This can benefit 
switching between different data to test
- Well-written code: strong convention, reusable codes and usage of inheritance in several cases (Item and LevelConditions)
- Cute texture
- Usage of Extension and Util class to improve reusability of the code.

# Disadvantage
- Item class is hard to debug. This is due to it being a pure C# class, with extra View gameObject to display the model.
- Usage of Resource.Load() and string-based asset loading. This is known to be very slow compare to direct referencing 
through ScriptableObject or Prefabs.
- Code coupling is very tight. The UI require many layer 
- Folder structure is not strong. Texture was not organized into different folder. Usage of Resource folder (like above)
Several folder are similarly named (Utilities and Utility).
- Usage of Instantiate and Destroy whenever "matches" happens or reloading the entire game. (Deleting the entire board
after playing is not performance-optimal)
- Usage of multiple prefabs for the same item/preset with the same purpose. Example: Item Prefabs have 7 variation, 
when the difference is only changing out the visual.

# Suggestion
- Item class should only be a data container for currently held Item in a Cell. The Cell should
be the class handling visual of the item.
- Increase usage of ScriptableObject. GameSettings can be drag into the inspector, and can minimize
code coupling. 
- UI data binding can be separated from the GameManager, with usage of Model - View - Controller design pattern.
- Reusing instantiated objects, or setting up the board pre-game and reusing it.
- Increase usage of unified prefabs with good resetting mechanism for Object Pooling.

