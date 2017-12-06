using ReturnToText.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReturnToText {
	enum STATE { MAP, MENU }
	class Game {

		static void Main(string[] args) {
			Instance=new Game();
		}

		public static Game Instance;
		public static bool Running = true;
		public static readonly Database Data = new Database();
		const string Title = "Return To Text";
		static readonly Screen screen = new Screen();
		public static STATE GameState = STATE.MAP;

		public static Dictionary<string, ConsoleKey> Keys = new Dictionary<string, ConsoleKey>() {
			{ "PositiveY", ConsoleKey.W}, { "NegativeY", ConsoleKey.S}, { "PositiveX", ConsoleKey.D }, { "NegativeX", ConsoleKey.A }, { "Select", ConsoleKey.Enter },
			{ "Return", ConsoleKey.Backspace }
		};
		public static ConsoleKey[] MovementKeys {
			get { return new ConsoleKey[] { Keys["PositiveY"], Keys["NegativeY"], Keys["PositiveX"], Keys["NegativeX"] }; }
		}

		public static Map Level;
		public static GUIController Menu;

		public Game() {
			Console.CursorVisible=false;
			new Player();
			Thread Rend = new Thread(new ThreadStart(Render)) {
				Name="Render"
			};
			Rend.Start();
			Thread Inp = new Thread(new ThreadStart(getInput)) {
				Name="GetInputs"
			};
			Inp.Start();
			Level=Data.getMap("0");
			Level.setPlayerLoc(3, 3);
			Tick();
		}

		Queue<ConsoleKey> Inputs = new Queue<ConsoleKey>(16);
		public void getInput() {
			while (Running) {
				Inputs.Enqueue(Console.ReadKey(true).Key);
			}
		}

		#region Display

		const int frameBuffer = 8;
		Queue<string> screenBuffer = new Queue<string>(frameBuffer);
		public void Render() {
			Stopwatch time = new Stopwatch();
			long elapsed = 0;
			int frames = 0;
			time.Start();
			while (Running) {
				elapsed+=time.ElapsedMilliseconds;
				frames++;
				time.Restart();

				if (screenBuffer.Count!=0) {
					string frame = screenBuffer.Dequeue();
					screen.Write(frame, 0);
					screen.Flip();
				} else {
					Thread.Sleep(100);
				}

				if (elapsed>=1000.0) {
					Console.Title=Title+" | FPS:"+frames.ToString();
					frames=0;
					elapsed=0;
				}
			}
		}
		public void Display(string frame) {
			lock (screenBuffer) {
				if (screenBuffer.Count==frameBuffer) {
					screenBuffer.Dequeue();
				}
				screenBuffer.Enqueue(frame);
			}
		}

		#endregion

		public void Tick() {
			while (Running) {
				Display(Level.getDisplay());
				if (Inputs.Count>4) {
					handleInputs(4);
				} else {
					handleInputs(Inputs.Count);
				}
			}
		}

		void handleInputs(int num) {
			for (int i = 0; i<num; i++) {
				ConsoleKey inp = Inputs.Dequeue();
				if (MovementKeys.Contains(inp) || inp==Keys["Select"]) {
					if (GameState==STATE.MAP) {
						//Movement (Map)
						Level.doMovement(inp);
					}else if (GameState==STATE.MENU) {
						//Movement (Menu
						Menu.doInput(inp);
					}
				}
			}
		}
	}
}
