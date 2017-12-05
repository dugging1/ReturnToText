using ReturnToText.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.LittleLanguages.SkillInterpretation.Nodes {
	public interface INode {
		object Execute(SkillContext c);
	}
}
