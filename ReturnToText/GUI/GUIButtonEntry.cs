using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText.GUI {
	public class GUIButtonEntry:GUIElement {
		public delegate void GUIButtonCallback(GUIButtonEntry sender);

		public string Display { get; set; }
		public object data;
		public GUIButtonCallback callBack;

		public GUIButtonEntry(string disp, GUIButtonCallback cb, object data=null) {
			Display=disp;
			this.data=data;
			callBack=cb;
		}

		public void Call() {
			callBack(this);
		}
	}
}
