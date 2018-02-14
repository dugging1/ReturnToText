using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public class Map {

		public readonly int ID;
		public readonly int Width;
		public readonly int Height;

		public Tile[] Mapping;

		int[] playerLoc;

		public Map(int width, int height, int id) {
			Width=width;
			Height=height;
			ID=id;
			Mapping=new Tile[Width*Height];
		}

		public void GenerateMap(string TileLayer, string RegionLayer, Dictionary<string, Tile> tileSet) {
			string[] TileIDs = TileLayer.Split(',');
			string[] Regions = RegionLayer.Split(',');

			for (int x = 0; x<Width; x++) {
				for (int y = 0; y<Height; y++) {
					Mapping[y*Width+x]=new Tile(tileSet[TileIDs[y*Width+x]]);
					Mapping[y*Width+x].EnemySpawnRegion=Regions[y*Width+x];
				}
			}
		}

		public string getDisplay() {
			StringBuilder s = new StringBuilder();
			if (Width>51||Height>51) {
				for (int y = Math.Min(0,playerLoc[1]-25); y<Math.Min(Height,playerLoc[1]+25); y++) {
					for (int x = Math.Min(0, playerLoc[0]-25); x<Math.Min(Width,playerLoc[0]+25); x++) {
						s.Append(Mapping[y*Width+x].Display);
					}
					s.Append('\n');
				}
			} else {
				for (int y = 0; y<Height; y++) {
					for (int x = 0; x<Width; x++) {
						s.Append(Mapping[y*Width+x].Display);
					}
					s.Append('\n');
				}
			}
			return s.ToString();
		}

		public void setPlayerLoc(int x, int y) {
			if (playerLoc!=null) {
				Mapping[playerLoc[1]*Width+playerLoc[0]].hasPlayer=false;
			}
			Mapping[y*Width+x].hasPlayer=true;
			playerLoc=new int[] { x, y };
		}

		public void doMovement(ConsoleKey inp) {
			if (inp==Game.Keys["PositiveY"]) {
				if (Mapping[(playerLoc[1]-1)*Width+playerLoc[0]].Passable[2]) setPlayerLoc(playerLoc[0], playerLoc[1]-1);
			} else if (inp==Game.Keys["NegativeX"]) {
				if (Mapping[playerLoc[1]*Width+playerLoc[0]-1].Passable[1]) setPlayerLoc(playerLoc[0]-1, playerLoc[1]);
			} else if (inp==Game.Keys["NegativeY"]) {
				if (Mapping[(playerLoc[1]+1)*Width+playerLoc[0]].Passable[0]) setPlayerLoc(playerLoc[0], playerLoc[1]+1);
			} else if (inp==Game.Keys["PositiveX"]) {
				if (Mapping[playerLoc[1]*Width+playerLoc[0]+1].Passable[3]) setPlayerLoc(playerLoc[0]+1, playerLoc[1]);
			} else {
				return;
			}
			//On successful move
			Mapping[playerLoc[1]*Width+playerLoc[0]].doEvents();
		}
	}
}
