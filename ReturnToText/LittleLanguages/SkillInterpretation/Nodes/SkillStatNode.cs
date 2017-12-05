using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;

namespace ReturnToText.LittleLanguages.SkillInterpretation.Nodes {
	public class SkillStatNode : ISkillNode {
		public SkillType fighterReferance;
		public string Stat;

		public SkillStatNode(SkillType r, string s) {
			fighterReferance=r;
			Stat=s;
		}

		public object Execute(SkillContext c) {
			if (fighterReferance==SkillType.C) {
				return c.Caster.Stats[Stat];
			} else {
				return c.Target.Stats[Stat];
			}
		}
	}
}
