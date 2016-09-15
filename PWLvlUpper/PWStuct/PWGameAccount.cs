using System;
using System.Collections.Generic;

namespace PWLvlUpper.PWStuct
{
	internal class PWGameAccount
	{
		public string Login;

		public string Password;

		public bool Error;

		public System.DateTime CreationTime;

		public System.Collections.Generic.List<PWCharacter> Characters;

		public System.Collections.Generic.List<string> ProblemChars;

		public bool IsForbid;

		public PWRoleForbid Forbid;

		public PWGameAccount(string Login, string Password)
		{
			this.Login = Login;
			this.Password = Password;
			this.Characters = new System.Collections.Generic.List<PWCharacter>();
			this.ProblemChars = new System.Collections.Generic.List<string>();
		}

		public override string ToString()
		{
			return null;
		}
	}
}
