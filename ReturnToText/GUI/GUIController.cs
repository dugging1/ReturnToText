using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.GUI {
	public class GUIController {
		public delegate void GUIReturnCB();

		public GUIElement[] elements = new GUIElement[0];
		public int cursorPos = 0;
		public static bool Running = true;
		public int minCursor = 0;
		public int maxCursor = 0;
		public GUIReturnCB ReturnCB;

		public void AddElement(GUIElement ele) {
			GUIElement[] tmp = new GUIElement[elements.Length+1];
			for (int i = 0; i<elements.Length; i++) {
				tmp[i]=elements[i];
			}
			tmp[tmp.Length-1]=ele;
			elements=tmp;
			maxCursor++;
		}

		public void doInput(ConsoleKey inp) {
			if (inp==Game.Keys["PositiveY"]) {
				cursorPos--;
			}else if (inp==Game.Keys["NegativeY"]) {
				cursorPos++;
			}else if (inp==Game.Keys["Select"]) {
				if (elements.Length==0) return;
				if(elements[cursorPos] is GUIButtonEntry) {
					((GUIButtonEntry)elements[cursorPos]).Call();
				}
			}else if (inp==Game.Keys["Return"]) {
				if (ReturnCB!=null) ReturnCB();
			}
			if (cursorPos<minCursor) cursorPos=minCursor;
			if (cursorPos>=maxCursor) cursorPos=maxCursor;
		}

		public string Display() {
			StringBuilder ret = new StringBuilder();
			for (int i = 0; i<elements.Length; i++) {
				if (cursorPos==i) ret.Append(" --> ");
				ret.Append(elements[i].Display);
				ret.Append("\n");
			}
			return ret.ToString();
		}
	}
}
