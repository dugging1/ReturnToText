using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public class Tile {
		char FloorChar = '.';
		public bool[] Passable = new bool[] { true, true, true, true }; //Passable from N,E,S,W
		public string EnemySpawnRegion;
		public bool hasPlayer = false;

		public char Display{
			get {
				if (hasPlayer) return Player.Instance.DisplayIcon;
				return FloorChar;
			}
			set {
				FloorChar=value;
			}
		}

		public Tile() { }
		public Tile(Tile p) {
			FloorChar=p.FloorChar;
			Passable=new bool[] { p.Passable[0], p.Passable[1], p.Passable[2], p.Passable[3] };
			EnemySpawnRegion=p.EnemySpawnRegion;
			hasPlayer=p.hasPlayer;
		}
	}
}
