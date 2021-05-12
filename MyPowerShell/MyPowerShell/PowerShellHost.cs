#region References

using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;

#endregion

namespace MyPowerShell
{
	public class PowerShellHost : PSHost, IHostSupportsInteractiveSession
	{
		public PowerShellHost()
		{
			UI = new PowerShellHostUserInterface();
		}

		public override CultureInfo CurrentCulture { get; }

		public override CultureInfo CurrentUICulture { get; }

		public override Guid InstanceId { get; }

		public override string Name { get; }

		public override PSHostUserInterface UI { get; }

		public override Version Version { get; }

		public bool IsRunspacePushed { get; }

		public Runspace Runspace { get; }


		public void PopRunspace()
		{
			throw new NotImplementedException();
		}

		public void PushRunspace(Runspace runspace)
		{
			throw new NotImplementedException();
		}

		public override void EnterNestedPrompt()
		{
			throw new NotImplementedException();
		}

		public override void ExitNestedPrompt()
		{
			throw new NotImplementedException();
		}

		public override void NotifyBeginApplication()
		{
			throw new NotImplementedException();
		}

		public override void NotifyEndApplication()
		{
			throw new NotImplementedException();
		}

		public override void SetShouldExit(int exitCode)
		{
			throw new NotImplementedException();
		}
	}
}