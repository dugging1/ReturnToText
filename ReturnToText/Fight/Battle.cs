using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReturnToText.GUI;

namespace ReturnToText.Fight {
	enum BattleState{ Main, SkillList, ItemList, Target, End }

	public struct BattleCommand {
		Fighter caster;
		//Skill skill;
		Fighter target;
	}

	public class Battle {
		Fighter[] T1;
		Fighter[] T2;
		bool Turn; //True=T1, False=T2
		int current = 0;
		static BattleState state = BattleState.Main;
		BattleCommand currentCommand;

		public Battle(Fighter[] Team1, Fighter[] Team2, bool turn=false) {
			T1=Team1;
			T2=Team2;
			Turn=turn;
		}

		public static bool TeamDead(Fighter[] Team) {
			for (int i = 0; i<Team.Length; i++) {
				if (Team[i].Stats["HP"]!=0) return false;
			}
			return true;
		}

		#region MainMenu

		GUIController makeMainMenu() {
			List<GUIElement> ele = new List<GUIElement>();
			for (int i = 0; i<T1.Length; i++) {
				string s = T1[i].Name+"    HP: "+T1[i].Stats["HP"]+"/"+T1[i].Stats["MaxHP"]+"    MP: "+T1[i].Stats["MP"]+"/"+T1[i].Stats["MaxMP"];
				if (Turn&&current==i) {
					ele.Add(new GUIButtonEntry("   * "+s+" *   ", null));
				} else {
					ele.Add(new GUIButtonEntry(s, null));
				}
			}
			ele.Add(new GUIButtonEntry("", null));
			for (int i = 0; i<T2.Length; i++) {
				string s = T2[i].Name+"    HP: "+T2[i].Stats["HP"]+"/"+T2[i].Stats["MaxHP"]+"    MP: "+T2[i].Stats["MP"]+"/"+T2[i].Stats["MaxMP"];
				if (!Turn&&current==i) {
					ele.Add(new GUIButtonEntry("   * "+s+" *   ", null));
				} else {
					ele.Add(new GUIButtonEntry(s, null));
				}
			}

			ele.Add(new GUIButtonEntry("Fight", FightCB));
			ele.Add(new GUIButtonEntry("Item", ItemCB));
			return new GUIController() { elements=ele.ToArray(), minCursor=T1.Length+T2.Length+2, maxCursor=T1.Length+T2.Length+2+2 };
		}

		static void FightCB(GUIButtonEntry sender) {
			Battle.state=BattleState.SkillList;
		}

		static void ItemCB(GUIButtonEntry sender) {
			Battle.state=BattleState.ItemList;
		}

		#endregion

		#region SkillList

		static void SkillSelect(GUIButtonEntry sender) {
			//TODO: Handle skill select
		}

		static void SkillReturn() {
			state=BattleState.Main;
		}

		#endregion

		void doTurn(Fighter f) {
			if (f.Stats["HP"]==0) return;
			//TODO: Finnish
		}

		public bool mainLoop() {
			while (state!=BattleState.End) {
				for (int i = 0; i<T1.Length; i++) {
					doTurn(T1[i]);
				}
				for (int i = 0; i<T2.Length; i++) {
					doTurn(T2[i]);
				}
				if (TeamDead(T1)) return true;
				else return false;
			}
			//TODO: Handle escaping
			return true;
		}
	}
}
