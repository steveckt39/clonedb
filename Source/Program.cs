using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace clonedb
{
	class Program
	{
		static void Main(string[] args)
		{
			string constr = "User Id=irt;Password=irtd;Data Source=lsodev1";
			OracleTransaction ot;

			OracleConnection con = new OracleConnection(constr);
			con.Open();
			ot = con.BeginTransaction();

			// Need a proper casting for the return value when cloned
			OracleConnection clonedCon = (OracleConnection)ot.Connection.Clone();

			// Cloned connection is always closed, regardless of its source,
			//   But the connection string should be identical
			clonedCon.Open();
			if (clonedCon.ConnectionString.Equals(con.ConnectionString))
				Console.WriteLine("The connection strings are the same.");  // connection string is the same
			else
				Console.WriteLine("The connection strings are different.");

			// Close and Dispose OracleConnection object
			clonedCon.Dispose();
			//ot.Connection.Close();   // !! this Connection is NULL

			//clonedCon.Open();
			con.Close();            //  !! but this connection is still OK.
			con.Dispose();          
		}
	}
}
