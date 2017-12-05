﻿using ReturnToText.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.LittleLanguages.SkillInterpretation {
	public enum SkillType { NUM, PLUS, MINUS, MULTI, DIVIDE, POINT, C, T, STAT, EQUALS, EOF, LBRACKET, RBRACKET, POWER }
	public class SkillLexer {
		string input;
		int currentPos;
		char currentChar;

		public SkillLexer(string inp) {
			input=inp;
			currentPos=-1;
			advance();
		}

		void advance() {
			currentPos++;
			if (currentPos<input.Length) currentChar=input[currentPos];
			else currentChar='\0'; //NULL TERMINATING
		}

		#region Helpers
		Token Number() {
			StringBuilder s = new StringBuilder();
			while (Char.IsDigit(currentChar)) {
				s.Append(currentChar);
				advance();
			}
			return new Token() { Type=SkillType.NUM, Value=Convert.ToInt32(s.ToString()) };
		}

		void WhiteSpace() {
			while (Char.IsWhiteSpace(currentChar)) {
				advance();
			}
		}

		Token Operation() {
			switch (currentChar) {
				case '+':
					advance();
					return new Token() { Type=SkillType.PLUS, Value='+' };
				case '-':
					advance();
					return new Token() { Type=SkillType.MINUS, Value='-' };
				case '/':
					advance();
					return new Token() { Type=SkillType.DIVIDE, Value='/' };
				case '*':
					advance();
					return new Token() { Type=SkillType.MULTI, Value='*' };
				case '(':
					advance();
					return new Token() { Type=SkillType.LBRACKET, Value='(' };
				case ')':
					advance();
					return new Token() { Type=SkillType.RBRACKET, Value=')' };
				case '=':
					advance();
					return new Token() { Type=SkillType.EQUALS, Value='=' };
				case '.':
					advance();
					return new Token() { Type=SkillType.POINT, Value='.' };
				case '^':
					advance();
					return new Token() { Type=SkillType.POWER, Value='^' };
			}
			throw new Exception("How did you get here?");
		}

		Token Stat() {
			StringBuilder s = new StringBuilder();
			while (Char.IsLetter(currentChar)) {
				s.Append(currentChar);
				advance();
			}
			if (!Stats.StatNames.Contains(s.ToString())) throw new Exception("Unknown token in expression: "+s.ToString());
			return new Token() { Type=SkillType.STAT, Value=s.ToString() };
		}

		#endregion

		public Token nextToken() {
			while (currentChar!='\0') {
				if (Char.IsDigit(currentChar)) {
					return Number();
				}else if (Char.IsWhiteSpace(currentChar)) {
					WhiteSpace();
					continue;
				}else if ("+-*/()=.^".Contains(currentChar)) {
					return Operation();
				}else if ("tTcC".Contains(currentChar)) {
					if (Char.ToUpper(currentChar)=='T') {
						advance();
						return new Token() { Type=SkillType.T, Value='T' };
					} else {
						advance();
						return new Token() { Type=SkillType.C, Value='C' };
					}
				} else {
					return Stat();
				}
			}
			return new Token() { Type=SkillType.EOF, Value='\0' };
		}
	}
}
