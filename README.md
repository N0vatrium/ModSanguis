# At the time of writing the game is not even in early access, please don't report bugs to the devs if you use this mod

# ModSanguis - HF Csl

This is a quick mod the game [Ex Sanguis](https://store.steampowered.com/app/3275050/Ex_Sanguis/)

The current goal is to implement fun ideas as a way to familiarize myself with the codebase

## Features

**Nerfed healing**: you only heal half of your missing HP after a fight (value can be changed in the configs)
**Bravery**: you can't choose your talents, instead they are randomly picked (value can be changed in the configs, disabled by default)

## Known bugs
At the moment the healing is tied to the character name, I'm not sure yet if the IDs I found are stable, so don't use the same name multiple times

Also there is no persistence yet so you can cheese it by saving and reloading out of combat to clear the saved values

## Installation
If you already installed BepInEx for Ex Sanguis skip to step 5

1. Download [BepInEx 5.4.x.x](https://github.com/BepInEx/BepInEx/releases) (ex: BepInEx_win_x64_5.4.23.5.zip), 
2. Unzip everything at the root location of the game (ex C:\Program Files (x86)\Steam\steamapps\common\Ex Sanguis Playtest)
3. Start the game without any mods, wait for it to generate the files
4. Once loaded in the main menu close the game
5. [Get the DLLs](https://github.com/N0vatrium/ModSanguis/releases) and move them to BepInEx/plugins

## Config

1. Install the mod, run the game once then close it
2. Edit the file located in bepinex's config folder (ex C:\Program Files (x86)\Steam\steamapps\common\Ex Sanguis Playtest\BepInEx\config)
3. Save it
4. Launch the game
5. Config reloading will be added at a later time, until then you need to restart the game to apply them


## Technical note
If a dev is looking at this: there might be a bug with EntityGameplayAbilityComponent, the constructor is called twice when the combat starts  

Also I disabled the analytics to avoid messing with your stats
