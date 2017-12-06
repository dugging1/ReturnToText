using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.Fight {
	public class Fighter {
		public Stats Stats;
		public bool isAI;
		public doTurn AI;
		public string Name;
		public List<Skill> Skills = new List<Skill>();

		public delegate void doTurn(Fighter self, Fighter[] Ally, Fighter[] Enemy);

		public Fighter(string Name, Stats s, bool isAI, doTurn AI) {
			Stats=s;
			this.isAI=isAI;
			this.AI=AI;
		}
	}
}
