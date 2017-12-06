using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.Fight {
	public class Stats {
		public static readonly string[] StatNames = new string[] { "LVL", "MaxHP", "HP", "MaxMP", "MP", "ATK", "DEF" };
		Dictionary<string, int> stats;

		public int this[string i] {
			get { return stats[i]; }
			set { stats[i]=value; }
		}

		public Stats(int[] s) {
			if(s.Length !=StatNames.Length) throw new ArgumentOutOfRangeException("s", "The number of stats passed does not match the StatNames array length.");

			stats=new Dictionary<string, int>();
			for (int i = 0; i<StatNames.Length; i++) {
				stats[StatNames[i]]=s[i];
			}
		}

		public Stats(string s) {
			string[] S = s.Split(',');
			int[] t = new int[S.Length];
			for (int i = 0; i<S.Length; i++) {
				t[i]=Convert.ToInt32(S[i]);
			}

			if (t.Length!=StatNames.Length) throw new ArgumentOutOfRangeException("s", "The number of stats passed does not match the StatNames array length.");
			stats=new Dictionary<string, int>();
			for (int i = 0; i<StatNames.Length; i++) {
				stats[StatNames[i]]=s[i];
			}
		}
	}
}
