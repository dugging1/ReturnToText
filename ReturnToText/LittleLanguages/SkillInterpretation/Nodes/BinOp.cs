﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.Fight;
using ReturnToText.LittleLanguages.SkillInterpretation.Nodes;
using ReturnToText.LittleLanguages.SkillInterpretation;

namespace ReturnToText.LittleLanguages.SkillInterpretation {
	public class BinOp : ISkillNode {
		ISkillNode left;
		ISkillNode right;
		SkillType Operation;

		public BinOp(ISkillNode l, ISkillNode r, Token op) {
			left=l;
			right=r;
			Operation=op.Type;
		}

		public object Execute(SkillContext c) {
			double val = 0;
			double l = Convert.ToDouble(left.Execute(c));
			double r = Convert.ToDouble(right.Execute(c));
			switch (Operation) {
				case SkillType.PLUS:
					val=l+r;
					break;
				case SkillType.MINUS:
					val=l-r;
					break;
				case SkillType.MULTI:
					val=l*r;
					break;
				case SkillType.DIVIDE:
					val=l/r;
					break;
				case SkillType.POWER:
					val=Math.Pow(l, r);
					break;
			}
			return val;
		}
	}
}
