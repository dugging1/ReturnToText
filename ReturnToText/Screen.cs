using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnToText {
	public class Screen {
		string OnScreen = "";
		StringBuilder buffer = new StringBuilder();

		public void Write(string str, int pos) {
			if (str==null||pos<0) return;
			for(int i = 0; i<str.Length; i++) {
				if (pos+i<buffer.Length) { 
					buffer[pos+i]=str[i];
				} else {
					Write(str.Substring(i));
					break;
				}
			}
		}

		public void Write(string str) {
			buffer.Append(str);
		}

		public void WriteLine(string str, int pos) {
			Write(str, pos);
			buffer[pos+str.Length]='\n';
		}

		public void WriteLine(string str) {
			Write(str);
			buffer.Append('\n');
		}

		public void Flip() {
			string Buffer = buffer.ToString();
			
			int x = 0;
			int y = 0;
			for(int i = 0; i<Math.Max(buffer.Length, OnScreen.Length); i++) {
				if (i<OnScreen.Length && i<buffer.Length) {
					if(Buffer[i] !=OnScreen[i]) {
						Console.SetCursorPosition(x, y);
						Console.Write(Buffer[i]);
					}
				} else if(i>=OnScreen.Length) {
					string t = Buffer.Substring(i);
					Console.SetCursorPosition(x, y);
					Console.Write(t);
					break;
				} else if (i>=buffer.Length) {
					Console.SetCursorPosition(x, y);
					Console.Write(new string(' ', OnScreen.Length-buffer.Length));
					break;
				}
				x++;
				if (Buffer[i]=='\n') {
					x=0;
					y++;
				}
			}
			OnScreen=Buffer;
			buffer.Clear();
		}


	}
}
