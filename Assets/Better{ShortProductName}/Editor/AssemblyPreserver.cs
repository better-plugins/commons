using UnityEngine.Scripting;

/// <summary>
/// Internal placeholder class to ensure the assembly is not empty.
/// This prevents Unity from triggering 'Empty Assembly' warnings.
/// </summary>
[Preserve]
internal static class AssemblyPreserver
{
	// This static constructor ensures the assembly is treated as having code
	[Preserve]
	static AssemblyPreserver()
	{
	}
}