# Custom Tile Creator Windows

There are two windows, Tile Creator and Tile Editor.

[Home Page](https://github.com/EdwardDobson/DungeonGeneratorV2.0)

## [Tile Creator](https://github.com/EdwardDobson/DungeonGeneratorV2.0/blob/main/Assets/Scripts/Tiles/TileCreator.cs)

* The tile creator is used to create new custom tiles that you want to use in the generator.
* To add new sprite options add your sprite **(64x64 + Pixels Per Unit of 64 recommended)** to the sprite atlas found at Resources/TileTextures.
* After you have modifed the tile variables in the window click create tile. This will add it into a TileDatas file in the Resources folder, this might move in the future.
* If there is no file, one will be created when you make your first tile.

## [Tile Editor](https://github.com/EdwardDobson/DungeonGeneratorV2.0/blob/main/Assets/Scripts/TileEditor.cs)

* If you have at least one tile made, opening the Tile Editor window will give you options to change the tiles variables aswell as showing its ID.
* You can also remove the tile if you no longer need it.
* When removing a tile all of the other tile ids will be reordered.


## [Clear Data Script](https://github.com/EdwardDobson/DungeonGeneratorV2.0/blob/main/Assets/Scripts/ClearTileDataFile.cs)

Finally the **CLEAR ALL TILE DATAS removes the Tile Data file**.



# Features

- [ ] Code Comments

- [x] Tile Health

- [x] Tile Speed

- [x] Tile Type

- [x] Tile Colour

- [x] Tile Rarity

- [ ] Block Drop

- [x] Tile Sprite Selection

- [ ] Multiple Tile Sprites

- [ ] Block Sounds
