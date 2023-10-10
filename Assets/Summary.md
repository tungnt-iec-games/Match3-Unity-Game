# Summary

## Evaluation

### Advantages
- Game Manager initializes everythings which prevent initialization error.
- Seperation of board logic and level condition logic helps easier implementation of future level condition logic and easy testing of each part.
- Inheritance of level condition makes it clear of what each implementation of level condition needs.
- MVC structure for the board helps keeping data and logic in seperate places.

### Disadvantags
- Multiple prefab for each type of Item requires prefab manual creation each time a new Item is added to the game.
- Duplicate folder "Utilities" and "Utility" may cause asset mis-locations.

## Suggestion
- The UI system should have a Screen Manager to load and unload different screen at runtime instead of manually setup in scene.
- Textures folder should have multiple subfolders where each is responsible for the image usage(e.g: "Textures/ItemIcon", "Textures/Button"...)
