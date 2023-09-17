//Example 1
var fileStream = new FileStream(@"c:\Projects\test.txt", FileMode.Open);
var saveHandle = fileStream.SafeFileHandle.DangerousGetHandle();
Console.WriteLine(saveHandle);

//Example 2
var processes = System.Diagnostics.Process.GetProcesses();
foreach (var process in processes)
{
    Console.WriteLine(process.Handle);
}
