using ReturnToText.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public static class StandardEvents {
		public static void transportEvent(string[] args) {
			//args[] = { MapID, XLoc, YLoc }
			int mapID = Convert.ToInt32(args[0]);
			int XLoc = Convert.ToInt32(args[1]);
			int YLoc = Convert.ToInt32(args[2]);

			if (Game.Level.ID!=mapID) Game.Level=Game.Data.getMap(mapID.ToString());
			Game.Level.setPlayerLoc(XLoc, YLoc);
		}

		public static void textEvent(string[] args) {
			//args[] = { Line1, Line2, ... }
			GUIController g = new GUIController();
			GUIElement[] ele = new GUIElement[args.Length+2];
			for (int i = 0; i<args.Length; i++) {
				ele[i]=new GUIButtonEntry(args[i], null);
			}
			ele[ele.Length-2]=new GUIButtonEntry("", null);
			ele[ele.Length-1]=new GUIButtonEntry("Return", endTextButtonCB, Game.GameState);
			g.elements=ele;
			g.minCursor=ele.Length-1;
			Game.Menu=g;
			Game.GameState=STATE.MENU;
		}

		static void endTextButtonCB(GUIButtonEntry sender) {
			Game.GameState=(STATE)sender.data;
			Game.Menu=null;
		}

		public static void changeTileEvent(string[] args) {
			//args[] = { "Pos,", floorChar, "Passable,", EnemySpawnRegion } "null" = no change
			int x = Convert.ToInt32(args[0].Split(',')[0]);
			int y = Convert.ToInt32(args[0].Split(',')[1]);

			if (args[1]!="null") Game.Level.Mapping[Game.Level.Width*y+x].Display=args[1][0];
			if (args[2]!="null") {
				string[] p = args[2].Split(',');
				bool[] pass = new bool[4] { Convert.ToBoolean(p[0]), Convert.ToBoolean(p[1]), Convert.ToBoolean(p[2]), Convert.ToBoolean(p[3]) };
				Game.Level.Mapping[Game.Level.Width*y+x].Passable=pass;
			}
			if (args[3]!="null") Game.Level.Mapping[Game.Level.Width*y+x].EnemySpawnRegion=args[3];
		}
	}
}
