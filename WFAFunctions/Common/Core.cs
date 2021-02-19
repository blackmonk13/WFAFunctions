using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WFAFunctions.Common
{
    public class Core
    {
		public string Filetostring(string FilePath)
		{
			try
			{
				string line = null;
                TextReader readFile = new StreamReader(FilePath);
				line = readFile.ReadToEnd();
				readFile.Close();
				readFile = null;
				return line;
			}
			catch (IOException ex)
			{
                return ex.Message;
			}
		}

		public bool Validemail(string mailstr)
		{
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(mailstr, pattern))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
