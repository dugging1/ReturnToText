using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public class Event {
		public delegate void EventEffect(string[] args);
		public EventEffect[] StdEffects = new EventEffect[] { StandardEvents.transportEvent, StandardEvents.textEvent, StandardEvents.changeTileEvent };

		string[] arguments;
		int ID = -1;

		public Event(int id, params string[] args) {
			ID=id;
			arguments=args;
		}

		public Event(int id, string args) {
			ID=id;
			arguments=args.Split(';');
		}

		public void execute() {
			StdEffects[ID](arguments);
		}
	}
}
