using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReturnToText.LittleLanguages.SkillInterpretation;
using ReturnToText.Fight;
using System.Collections.Generic;

namespace LanguageUnitTests {
	[TestClass]
	public class UnitTest1 { 
		//TODO: Unit test Stats assignment

		[TestCategory("Skill Language")]
		[TestMethod]
		public void TestSkillLanguage_Maths() {
			List<string> statement = new List<string>();
			List<double> expected = new List<double>();
			SkillLexer lex;
			SkillParser parser;

			statement.Add("1+2;");
			expected.Add(3.0);

			statement.Add("2*2;");
			expected.Add(4);

			statement.Add("4/2;");
			expected.Add(2);

			statement.Add("1+2+3+4;");
			expected.Add(10);

			statement.Add("4/2*2;");
			expected.Add(4);

			statement.Add("2.2+1;");
			expected.Add(3.2);

			statement.Add("2^3;");
			expected.Add(8);

			for (int i = 0; i<statement.Count; i++) {
				lex=new SkillLexer(statement[i]);
				parser=new SkillParser(lex);
				double actual = Convert.ToDouble(((List<object>)parser.getTree().Execute(new SkillContext()))[0]);
				Assert.AreEqual(expected[i], actual);
			}
		}

		[TestCategory("Skill Language")]
		[TestMethod]
		public void TestSkillLanguage_StatsRetrieval() {
			List<string> statement = new List<string>();
			List<double> expected = new List<double>();
			List<SkillContext> context = new List<SkillContext>();
			SkillLexer lex;
			SkillParser parser;

			SkillContext baseContext = new SkillContext() { Caster=new Fighter("", new Stats(new int[] { 1, 2, 3, 4, 5, 6, 7 }), false, null), Target=new Fighter("", new Stats(new int[] { 7, 6, 5, 4, 3, 2, 1 }), false, null) };

			statement.Add("C.HP;");
			expected.Add(3);
			context.Add(baseContext);

			statement.Add("T.LVL;");
			expected.Add(7);
			context.Add(baseContext);

			statement.Add("T.DEF-C.ATK;");
			expected.Add(-5);
			context.Add(baseContext);

			for (int i = 0; i<statement.Count; i++) {
				lex=new SkillLexer(statement[i]);
				parser=new SkillParser(lex);
				double actual = Convert.ToDouble(((List<object>)parser.getTree().Execute(context[i]))[0]);
				Assert.AreEqual(expected[i], actual);
			}
		}
	}
}
