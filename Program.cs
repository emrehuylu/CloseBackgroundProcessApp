using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ClosedAppSon
{
    public class Program
	{
		static void Main(string[] args)
		{
			// C:\Users\Administrator\AppData\Roaming\CloseApp
			var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CloseApp"); // path.combine choose all files
			WriteLogs wrt = new WriteLogs();

			try
			{
                //exeName = "Apps.Projects.Tesla.Surface";
                var exePathApp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Apps.Projects.Tesla.Surface"); //find that want to close .exe path

				var fullpath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "LogDoc", "logs"); // path.combine choose all files in :/C, not appdata inside
				var fileNames = Directory.GetFiles(fullpath);

				int latestIndex = fileNames.Length - 1;   //last file index
				string latestFile = fileNames[latestIndex]; //last file indexName
                var destinationFile = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "LogDoc", "deneme", "deneme.log");
				if (File.Exists(destinationFile))
				{
					File.Delete(destinationFile);
				}
				File.Copy(latestFile, destinationFile);   // Copy to last .txt file

				var sourcepath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "LogDoc", "deneme"); // path.combine choose all files in :/C, not appdata inside
				var sourceFileNames = Directory.GetFiles(sourcepath);

				int latestIndexSource = sourceFileNames.Length - 1;   //last file index
                string latestFileSource = sourceFileNames[latestIndexSource]; //last file indexName

                string[] lines = File.ReadAllLines(latestFileSource);
				int startIndex = Math.Max(lines.Length - 10, 0);

				int count = 0;
				string textline = "FridgeStopped is changed to true.";
				List<string> lastTenLines = new List<string>();
				wrt.WriteLogsMethod("CloseApp.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", "Number of items added to list -->", lastTenLines.Count), logPath);

				for (int i = startIndex; i < lines.Length; i++)
				{
					if (lines[i].Contains(textline))
					{
						count++;
						lastTenLines.Add(textline);
					}
				}
				if (lastTenLines.Count >= 10)
				{
					string exeName = System.IO.Path.GetFileNameWithoutExtension(exePathApp);

					foreach (var allProcess in Process.GetProcessesByName(exeName))
					{
						allProcess.Kill();  //master app close
						Task.Delay(3000);
						Process.Start(exeName);  //master app .exe start again
						wrt.WriteLogsMethod("CloseApp.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", "Close & Open situations works in this time -->", exeName), logPath);
						break;
					}
				}
				wrt.WriteLogsMethod("CloseApp.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", "Code of cycle is worked. -->", true), logPath);
				Environment.Exit(0);
			}
			catch (Exception ex)
			{
				wrt.WriteLogsMethod("CloseApp.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", "ExceptionCatch -->", ex.Message), logPath);
			}
			finally
			{
				wrt.WriteLogsMethod("CloseApp.log", String.Format("{0}{1}{2} {3} {4}", "[", DateTime.Now, "]", "ExceptionFinal -->", true), logPath);
			}
		}
	}
}
