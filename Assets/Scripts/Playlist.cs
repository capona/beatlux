using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;

public class Playlist : MonoBehaviour {

	// Database connection
	IDbConnection db;


	// Playlists
	List<PlaylistObj> playlists = new List<PlaylistObj>();

	void Start ()
	{
		// Connect to database
		DbConnect ();

		// Select playlists from database
		LoadPlaylists ();
		LoadPlaylists ();
	}

	bool DbConnect ()
	{
		if (db == null) {
			db = Database.GetConnection ();
		}

		return db != null;
	}

	void DbClose ()
	{
		// Close database
		Database.Close ();

		// Reset database instance
		db = null;
	}

	void LoadPlaylists ()
	{
		if (DbConnect ())
		{
			// Database command
			IDbCommand cmd = db.CreateCommand ();

			// Query statement
			string sql = "SELECT id,name,files FROM playlist";
			cmd.CommandText = sql;

			// Get sql results
			IDataReader reader = cmd.ExecuteReader ();

			// Read sql results
			while (reader.Read ())
			{
				// Create playlist object
				PlaylistObj obj = new PlaylistObj ();

				// Set id and name
				obj.ID = reader.GetInt32 (0);
				obj.Name = reader.GetString (1);

				// Get file IDs
				string[] fileIDs = !reader.IsDBNull (2) ? reader.GetString (2).Split (new Char[] { ',', ' ' }) : new string[0];

				// Select files
				List<String> files = new List<String> ();
				foreach (string id in fileIDs)
				{
					// Send database query
					IDbCommand cmd2 = db.CreateCommand ();
					cmd2.CommandText = "SELECT path FROM file WHERE id = '" + id + "'";
					IDataReader fileReader = cmd2.ExecuteReader ();

					// Read and add file paths
					while (fileReader.Read ()) {
						files.Add (fileReader.GetString (0));
					}

					// Close reader
					fileReader.Close ();
					cmd2.Dispose ();
				}

				// Set files
				obj.Files = files.ToArray();

				// Add contents to playlists array
				playlists.Add (obj);
			}

			// Close reader
			reader.Close ();
			cmd.Dispose ();

			// Close database connection
			DbClose ();
		}
	}
}
