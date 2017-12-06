using ReturnToText.LittleLanguages.SkillInterpretation;
using ReturnToText.LittleLanguages.SkillInterpretation.Nodes;
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
		public ISkillNode effect;

		public void interpretEffect(string effect) {
			SkillLexer lex = new SkillLexer(effect);
			SkillParser parser = new SkillParser(lex);
			this.effect=parser.getTree();
		}
	}
}
