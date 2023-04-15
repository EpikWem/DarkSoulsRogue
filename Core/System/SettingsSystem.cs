﻿using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace DarkSoulsRogue.Core.System;

public class SettingsSystem
{
    
    private const string SettingsFilePath = Main.ContentPath + @"saves\settings.xml";
    private const string DefaultFilePath = Main.ContentPath + @"saves\default_settings.xml";
    public const int SavesCount = 4;

    private static readonly XmlDocument SettingsFile = new();
    
    public static void Init()
    {
        SettingsFile.Load(SettingsFilePath);
    }



    /**=============================================================
     *= LOAD METHOD ================================================
     *============================================================*/

    public static void Load()
    {
        SetupControl(Control.Up, "up");
        SetupControl(Control.Down, "down");
        SetupControl(Control.Right, "right");
        SetupControl(Control.Left, "left");
        SetupControl(Control.Interact, "interact");
        SetupControl(Control.Run, "run");
        SetupControl(Control.Weapon, "weapon");
        SetupControl(Control.Shield, "shield");
        SetupControl(Control.Consumable, "consumable");
        SetupControl(Control.Catalyst, "catalyst");
        SetupControl(Control.MenuUp, "menuUp");
        SetupControl(Control.MenuDown, "menuDown");
        SetupControl(Control.MenuRight, "menuRight");
        SetupControl(Control.MenuLeft, "menuLeft");
        SetupControl(Control.MenuBack, "menuBack");
    }

    

    /**=============================================================
     *= SAVE METHOD ================================================
     *============================================================*/
    
    public static void Save()
    {
        SaveControl(Control.Up, "up");
        SaveControl(Control.Down, "down");
        SaveControl(Control.Right, "right");
        SaveControl(Control.Left, "left");
        SaveControl(Control.Interact, "interact");
        SaveControl(Control.Run, "run");
        SaveControl(Control.Weapon, "weapon");
        SaveControl(Control.Shield, "shield");
        SaveControl(Control.Consumable, "consumable");
        SaveControl(Control.Catalyst, "catalyst");
        SaveControl(Control.MenuUp, "menuUp");
        SaveControl(Control.MenuDown, "menuDown");
        SaveControl(Control.MenuRight, "menuRight");
        SaveControl(Control.MenuLeft, "menuLeft");
        SaveControl(Control.MenuBack, "menuBack");
        SettingsFile.Save(SettingsFilePath);
    }

    
    
    /**=============================================================
     *= OTHER METHODS ==============================================
     *============================================================*/
    
    public static void ResetToDefault()
    {
        SettingsFile.Load(DefaultFilePath);
        Load();
        SettingsFile.Save(SettingsFilePath);
    }

    private static void SetupControl(Control control, string node)
        => control.Set((Keys)GetInt(SettingsFile["XnaContent"]?["Asset"]?["controls"]?[node]));
    private static void SaveControl(Control control, string node)
        => SettingsFile["XnaContent"]!["Asset"]!["controls"]![node]!.InnerText = control.KeyCode().ToString();

    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    
}