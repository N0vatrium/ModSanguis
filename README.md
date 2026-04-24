# At the time of writing the game is not even in early access, please don't report bugs to the devs if you use this mod

# ModSanguis - HF Csl

This is a quick mod the game [Ex Sanguis](https://store.steampowered.com/app/3275050/Ex_Sanguis/)

The current goal is to implement fun ideas as a way to familiarize myself with the codebase

## Feature(s)

**Nerfed healing**: you only heal half of you missing HP after a fight

## Known bugs
I'm currently using absolute values for the HP instead of percentage, meaning that if you increase your max HP between 2 fights the result might be off. This is a quick fix that I intend to implement ASAP

At the moment the healing is tied to the character name, I'm not sure yet if the IDs I found are stable, so don't use the same name multiple times

Also there is no persistence yet so you can cheese it by saving and reloading out of combat to clear the saved values

## Installation
If you already installed BepInEx for Ex Sanguis skip to step 5

1. Download [BepInEx 5.4.x.x](https://github.com/BepInEx/BepInEx/releases) (ex: BepInEx_win_x64_5.4.23.5.zip), 
2. Unzip everything at the root location of the game (ex C:\Program Files (x86)\Steam\steamapps\common\Ex Sanguis Playtest)
3. Start the game without any mods, wait for it to generate the files
4. Once loaded in the main menu close the game
5. [Get the DLLs](https://github.com/N0vatrium/ModSanguis/releases) and move them to BepInEx/plugins


## Technical note
If a dev is looking at this: there might be a bug with EntityGameplayAbilityComponent, the constructor is called twice when the combat starts
