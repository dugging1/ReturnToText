using ReturnToText.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public class Player {
		public static Player Instance;

		public char DisplayIcon = '@';
		public Fighter[] Ally = new Fighter[4];

		public Player() {
			Instance=this;
			Ally[0]=new Fighter("Player", new Stats(new int[] { 1, 10, 10, 4, 4, 5, 2 }), false, null);
		}
	}
}
