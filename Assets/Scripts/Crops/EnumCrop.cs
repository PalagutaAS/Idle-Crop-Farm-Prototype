using System;

[Flags]
public enum CropType
{
    None = 0,
    Wheat = 1 << 0,
    Corn = 1 << 1,
    Potato = 1 << 2,
}
[Flags]
public enum MoneyType
{
    None = 0,
    Coin = 1 << 28,
    Emerald = 1 << 29,
}

[Flags]
public enum ToolType
{
    None = 0,
    Pickaxe = 1 << 14,
    Axe = 1 << 15,
    Shovel = 1 << 16,
}
[Flags]
public enum InventoryType
{
    None = CropType.None,
    Wheat = CropType.Wheat,
    Corn = CropType.Corn,
    Potato = CropType.Potato,
    Coin = MoneyType.Coin,
    Emerald = MoneyType.Emerald,
    Pickaxe = ToolType.Pickaxe,
    Axe = ToolType.Axe,
    Shovel = ToolType.Shovel,
}