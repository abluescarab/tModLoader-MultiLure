# MultiLure
**MultiLure** is a mod that adds a configurable number of lures while fishing and allows you to carry up to 99 quest fish in your inventory. MultiLure now includes fishing line accessories for those who don't like cheating. *(Thanks to Dark Dragoon's [Multi Lure Accessories mod](https://forums.terraria.org/index.php?threads/multi-lure-accessories.53754/) for the idea!)*

**Note:** if a quest fish is in your inventory when you cast, you still can't catch any more than what you're already carrying. Drop the existing fish or put them in a chest and try again.

* Includes Cheat Sheet and HERO's Mod integration (hotkeys work with or without integration)
* Press [ to remove a lure
* Press ] to add a lure
* Hold shift to add/remove 10 lures (works with both hotkeys and Cheat Sheet/HERO's Mod)
* Craft fishing lines using a [String](https://terraria.fandom.com/wiki/Strings), a [Hook](https://terraria.fandom.com/wiki/Hook), and 5 metal bars when "Enable fishing lines" is on

Each accessory offers:
| Fishing Line        | Total Lures |
| ------------------- | :---------: |
| Copper/Tin          | 5           |
| Iron/Lead           | 10          |
| Silver/Gold         | 25          |
| Cobalt/Palladium    | 50          |
| Mythril/Orichalcum  | 75          |
| Adamantite/Titanium | 100         |

## Configuration File
You can configure a number of options for MultiLure. The configuration file is located at `Documents\My Games\Terraria\ModLoader\ModConfigs`.

Available options:

* **EnableHotKeys** - enable or disable the [ and ] hotkeys
* **EnableFishingLines** - add equippable fishing line accessories
* **EnableCheatSheetIntegration** - enable Cheat Sheet integration
* **EnableHerosModIntegration** - enable HERO's Mod integration

### Cheat Configuration
```
{
  "EnableHotkeys": true,
  "EnableFishingLines": false,
  "EnableCheatSheetIntegration": true,
  "EnableHerosModIntegration": true
}
```

### Non-Cheat Configuration
```
{
  "EnableHotkeys": false,
  "EnableFishingLines": true,
  "EnableCheatSheetIntegration": false,
  "EnableHerosModIntegration": false
}
```

## Credits
* Nomad000 for the suggestion in the Terraria Forums
* jopojelly for Cheat Sheet
* HERO for HERO's Mod
* VVV101 for releasing updated version
* Dark Dragoon for Multi Lure Accessories
