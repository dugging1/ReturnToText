using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.Fight {
	public struct SkillContext {
		public Fighter Caster;
		public Fighter Target;
	}

	public class Skill {
		public string Name;
		public int MPConsumption;
		public int HPConsumption;
		public Stats extraData;
	}
}
