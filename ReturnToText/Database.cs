using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ReturnToText.Fight;

namespace ReturnToText {
	public class Database {
		SQLiteConnection data = new SQLiteConnection("Data Source=Data.db;Version=3;FailIfMissing=True;Read Only=True;");

		public Database() {
			data.Open();
		}

		public Fighter[] getTroop(string Region) {
			SQLiteCommand command = new SQLiteCommand("SELECT Mobs FROM Region WHERE ID="+Region, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("Region", Region, "Region given cannot be found.");
			r.Read();
			string[] m = r.GetString(0).Split(',');
			Random rand = new Random();
			string T = m[rand.Next(m.Length)];

			command=new SQLiteCommand("SELECT Mobs FROM Troop WHERE ID="+T, data);
			r=command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("Troop retrieved", T, "Troop retireved cannot be found.");
			r.Read();
			string[] Troop = r.GetString(0).Split(',');

			Fighter[] ret = new Fighter[Troop.Length];
			for (int i = 0; i<Troop.Length; i++) {
				ret[i]=getMob(Troop[i]);
			}
			return ret;
		}

		public Fighter getMob(string mobID) {
			SQLiteCommand command = new SQLiteCommand("SELECT * FROM MOBS WHERE ID="+mobID, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("mobID", mobID, "Mob given cannot be found.");
			r.Read();
			string n = r.GetString(1);
			int[] s = new int[r.FieldCount-2];
			for (int i = 2; i<r.FieldCount; i++) {
				s[i-2]=r.GetInt32(i);
			}
			return new Fighter(n, new Stats(s), true, null); //TODO: ADD AI
		}

		public Map getMap(string mapID) {
			SQLiteCommand command = new SQLiteCommand("SELECT * FROM Map WHERE ID="+mapID, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("mapID", mapID, "Map given cannot be found.");
			r.Read();

			int w = r.GetInt32(1);
			int h = r.GetInt32(2);
			string vis = r.GetString(3);
			string reg = r.GetString(4);
			Dictionary<string, Tile> tileSet = new Dictionary<string, Tile>();

			string[] Vis = vis.Split(',');
			List<int> IDs = new List<int>();
			for (int i = 0; i<Vis.Length; i++) {
				int ID = Convert.ToInt32(Vis[i]);
				if (!IDs.Contains(ID)) {
					tileSet[Vis[i]] = getTile(Vis[i]);
					IDs.Add(ID);
				}
			}

			Map ret = new Map(w, h);
			ret.GenerateMap(vis, reg, tileSet);
			return ret;
		}

		public Tile getTile(string tileID) {
			SQLiteCommand command = new SQLiteCommand("SELECT * FROM Tile WHERE ID="+tileID, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("tileID", tileID, "Tile given cannot be found.");
			r.Read();
			string[] tmp = r.GetString(2).Split(',');
			bool[] pass = new bool[4];
			for (int i = 0; i<4; i++) {
				if (tmp[i]=="0") pass[i]=false;
				else pass[i]=true;
			}
			return new Tile() { Display=r.GetString(1)[0], Passable=pass };
		}
	}
}
