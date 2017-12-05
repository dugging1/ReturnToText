using ReturnToText.LittleLanguages.SkillInterpretation.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.LittleLanguages.SkillInterpretation {
	public class SkillParser {
		SkillLexer Lexer;
		Token currentToken;

		public SkillParser(SkillLexer lex) {
			Lexer=lex;
			currentToken=lex.nextToken();
		}

		void Eat(SkillType t) {
			if(currentToken.Type ==t) {
				currentToken=Lexer.nextToken();
			} else {
				throw new Exception("Unexpected token of type: "+currentToken.Type.ToString()+" and value: "+currentToken.Value+" expected: "+t.ToString());
			}
		}

		//public ISkillNode getTree() {

		//}

		#region MathsDerivation

		INode Maths() {
			return Add();
		}

		INode Add() {
			INode n = Mult();
			while (currentToken.Type==SkillType.PLUS||currentToken.Type==SkillType.MINUS) {
				Token t = currentToken;
				Eat(t.Type);
				n=new BinOp(n,Add(), t);
			}
			return n;
		}

		INode Mult() {
			INode n = Pow();
			while (currentToken.Type==SkillType.MULTI||currentToken.Type==SkillType.DIVIDE) {
				Token t = currentToken;
				Eat(t.Type);
				n=new BinOp(n, Mult(), t);
			}
			return n;
		}

		INode Pow() {
			INode n;
			if(currentToken.Type ==SkillType.LBRACKET) {
				n=Brack();
			} else {
				n=Num();
			}
			while (currentToken.Type==SkillType.POWER) {
				Token t = currentToken;
				Eat(SkillType.POWER);
				n=new BinOp(n, Mult(), t);
			}
			return n;
		}

		INode Brack() {
			Eat(SkillType.LBRACKET);
			INode n = Maths();
			Eat(SkillType.RBRACKET);
			return n;
		}

		INode Num() {
			if (currentToken.Type==SkillType.NUM) {
				return Lit();
			} else {
				return Stat();
			}
		}

		INode Lit() {
			double v = Convert.ToDouble(currentToken.Value);
			Eat(SkillType.NUM);
			if (currentToken.Type==SkillType.POINT) {
				Eat(SkillType.POINT);
				double dec = Convert.ToDouble(currentToken.Value);
				Eat(SkillType.NUM);
				v=v+Math.Pow(10, -dec.ToString().Length)*dec;
			}
			return new Number(v);
		}

		INode Stat() {
			SkillType t;
			if (currentToken.Type==SkillType.C) {
				t=SkillType.C;
				Eat(SkillType.C);
			} else {
				t=SkillType.T;
				Eat(SkillType.T);
			}
			Eat(SkillType.POINT);
			string s = currentToken.Value.ToString();
			Eat(SkillType.STAT);
			return new SkillStatNode(t, s);
		}

		#endregion
	}
}
