using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;

namespace ReturnToText.LittleLanguages.SkillInterpretation.Nodes {
	public class Number : INode {
		double value;

		public Number(double val) {
			value=val;
		}

		public object Execute(SkillContext c) {
			return value;
		}
	}
}
