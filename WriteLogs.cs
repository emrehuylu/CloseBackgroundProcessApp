using System;
using System.IO;

namespace ClosedAppSon
{
    public class WriteLogs
	{
		/// <summary>
		/// Logger Class
		/// </summary>
		/// <param name="strFileName"></param>
		/// <param name="strMessage"></param>
		/// <param name="logPath"></param>
		/// <returns></returns>
		public bool WriteLogsMethod(string strFileName, string strMessage, string logPath)
		{
			try
			{
				using (FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Path.GetFullPath(logPath), strFileName), FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream))
					{
						objStreamWriter.WriteLine(strMessage);
						objStreamWriter.Close();
						objFilestream.Close();
					}
				}
				return true;
			}

			catch (Exception)
			{
				return false;
			}
		}

	}
}
