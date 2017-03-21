using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database {

	// Class instance
	private static Database instance;

	// Database name
	private static string name = "beatlux";

	// Database extension
	private static string ext = "db";

	// Database connection
	private static IDbConnection con;



	private Database ()
	{
		// Path to database
		string uri = "URI=file:" + Application.dataPath + "/" + name + "." + ext;

		// Connect to database
		con = connect(uri);
	}

	private IDbConnection connect(string uri)
	{
		// Connect to database
		con = (IDbConnection) new SqliteConnection (uri);

		// Open database connection
		con.Open();

		return con;
	}



	// Get instance (Singleton)
	public static IDbConnection getConnection ()
	{
		// Create instance if not exists
		if (instance == null) {
			instance = new Database ();
		}

		// Return database connection
		return con;
	}
}
