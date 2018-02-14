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

		public ISkillNode getTree() {
			return Block();
		}

		ISkillNode Block() {
			BlockNode b = new BlockNode();
			while(currentToken.Type !=SkillType.EOF) {
				b.AddStatement(Statement());
				Eat(SkillType.SEMI);
			}
			return b;
		}

		ISkillNode Statement() {
			if (Lexer.Peek().Type==SkillType.EQUALS) {
				return Assignment();
			} else {
				return Maths();
			}
		}

		ISkillNode Assignment() {
			SkillStatNode n = Stat() as SkillStatNode;
			Eat(SkillType.EQUALS);
			return new SkillAssignment(n, Maths());
		}

		#region MathsDerivation

		ISkillNode Maths() {
			return Add();
		}

		ISkillNode Add() {
			ISkillNode n = Mult();
			while (currentToken.Type==SkillType.PLUS||currentToken.Type==SkillType.MINUS) {
				Token t = currentToken;
				Eat(t.Type);
				n=new BinOp(n, Mult(), t);
			}
			return n;
		}

		ISkillNode Mult() {
			ISkillNode n = Pow();
			while (currentToken.Type==SkillType.MULTI||currentToken.Type==SkillType.DIVIDE) {
				Token t = currentToken;
				Eat(t.Type);
				n=new BinOp(n, Pow(), t);
			}
			return n;
		}

		ISkillNode Pow() {
			ISkillNode n;
			if(currentToken.Type ==SkillType.LBRACKET) {
				n=Brack();
			} else {
				n=Num();
			}
			while (currentToken.Type==SkillType.POWER) {
				Token t = currentToken;
				Eat(SkillType.POWER);
				n=new BinOp(n, Pow(), t);
			}
			return n;
		}

		ISkillNode Brack() {
			Eat(SkillType.LBRACKET);
			ISkillNode n = Maths();
			Eat(SkillType.RBRACKET);
			return n;
		}

		ISkillNode Num() {
			if (currentToken.Type==SkillType.NUM) {
				return Lit();
			} else {
				return Stat();
			}
		}

		ISkillNode Lit() {
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

		ISkillNode Stat() {
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
