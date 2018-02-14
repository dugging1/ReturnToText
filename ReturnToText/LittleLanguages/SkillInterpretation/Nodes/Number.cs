using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;
using ReturnToText.LittleLanguages.SkillInterpretation.Nodes;

namespace ReturnToText.LittleLanguages.SkillInterpretation {
	public class Number : ISkillNode {
		double value;

		public Number(double val) {
			value=val;
		}

		public object Execute(SkillContext c) {
			return value;
		}
	}
}
