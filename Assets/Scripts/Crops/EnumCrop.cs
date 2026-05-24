using System;

[Flags]
public enum CropType
{
    None = 0,
    Wheat = 1 << 0,
    Corn = 1 << 1,
    Potato = 1 << 2,
    Pumpkin = 1 << 4,
}
[Flags]
public enum MoneyType
{
    None = 0,
    Coin = 1 << 0,
    Emerald = 1 << 1,
}

[Flags]
public enum ToolType
{
    None = 0,
    Scythe = 1 << 0,
    Axe = 1 << 1,
    Shovel = 1 << 2,
}