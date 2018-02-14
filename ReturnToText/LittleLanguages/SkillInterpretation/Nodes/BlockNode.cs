using ReturnToText.LittleLanguages.SkillInterpretation.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;

namespace ReturnToText.LittleLanguages.SkillInterpretation {
	public class BlockNode : ISkillNode {
		public List<ISkillNode> statements = new List<ISkillNode>();

		public void AddStatement(ISkillNode s) {
			statements.Add(s);
		}

		public object Execute(SkillContext c) {
			List<object> res = new List<object>();
			foreach (ISkillNode i in statements) {
				res.Add(i.Execute(c));
			}
			return res;
		}
	}
}
