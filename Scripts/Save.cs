using Godot;
using System;

public static class Save
{
	public static void SaveGame()
	{
		var file = FileAccess.Open("user://savegame.txt", FileAccess.ModeFlags.Write);
		file.StoreLine(DateTime.Now.ToString());
		file.Close();

	}
}
