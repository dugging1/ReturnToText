using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;

namespace ReturnToText.LittleLanguages.SkillInterpretation.Nodes {
	class SkillAssignment : ISkillNode {
		public SkillStatNode left;
		public ISkillNode right;

		public SkillAssignment(ISkillNode l, ISkillNode r) {
			left=l as SkillStatNode;
			right=r;
		}

		public object Execute(SkillContext c) {
			if(left.fighterReferance ==SkillType.C) {
				c.Caster.Stats[left.Stat]=Convert.ToInt32(right.Execute(c));
			} else {
				c.Target.Stats[left.Stat]=Convert.ToInt32(right.Execute(c));
			}
			return null;
		}
	}
}
