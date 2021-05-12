using System;
using System.Management.Automation.Host;

namespace MyPowerShell
{
	public class PowerShellHostRawUserInterface : PSHostRawUserInterface
	{
		private readonly PowerShellHostUserInterface _userInterface;
		
		public PowerShellHostRawUserInterface(PowerShellHostUserInterface userInterface)
		{
			_userInterface = userInterface;
		}
		
		public override ConsoleColor BackgroundColor { get; set; }

		public override Size BufferSize { get; set; }

		public override Coordinates CursorPosition { get; set; }

		public override int CursorSize { get; set; }

		public override ConsoleColor ForegroundColor { get; set; }

		public override bool KeyAvailable { get; }

		public override Size MaxPhysicalWindowSize => WindowSize;

		public override Size MaxWindowSize => WindowSize;

		public override Coordinates WindowPosition { get; set; }

		public override Size WindowSize { get; set; }

		public override string WindowTitle { get; set; }


		public override void FlushInputBuffer()
		{
		}

		public override BufferCell[,] GetBufferContents(Rectangle rectangle)
		{
			return null;
		}

		public override KeyInfo ReadKey(ReadKeyOptions options)
		{
			return default;
		}

		public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip,
			BufferCell fill)
		{
		}

		public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
		{
		}

		public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
		{
		}
	}
}