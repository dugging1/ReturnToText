﻿using System;
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

			Map ret = new Map(w, h, Convert.ToInt32(mapID));
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

			List<Event> Es = new List<Event>();
			foreach (string id in Convert.ToString(r.GetValue(3)).Split(',')) {
				if(id != "") Es.Add(getEvent(id));
			}
			return new Tile() { Display=r.GetString(1)[0], Passable=pass, Events=Es.ToArray() };
		}

		public Event getEvent(string eventID) {
			SQLiteCommand command = new SQLiteCommand("SELECT * FROM Event WHERE ID="+eventID, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("eventID", eventID, "Event given cannot be found.");
			r.Read();
			string idStr = r.GetString(1);
			int id = 0;
			if (idStr.Contains("ID=")) {
				id = Convert.ToInt32(idStr.Substring(3));
			} else {
				throw new NotImplementedException("Event language not implemented.");
			}
			string[] args = r.GetString(2).Split(',');

			return new Event(id, args);
		}

		public Skill getSkill(string skillID) {
			SQLiteCommand command = new SQLiteCommand("SELECT * FROM Skill WHERE ID="+skillID, data);
			SQLiteDataReader r = command.ExecuteReader();
			if (!r.HasRows) throw new ArgumentOutOfRangeException("skillID", skillID, "Skill given cannot be found.");
			r.Read();

			Skill ret = new Skill() {
				Name=r.GetString(1),
				MPConsumption=r.GetInt32(2),
				HPConsumption=r.GetInt32(3),
				extraData=new Stats(r.GetString(4))
			};
			ret.interpretEffect(r.GetString(5));
			return ret;
		}
	}
}
