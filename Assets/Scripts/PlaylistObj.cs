using UnityEngine;
using System.Collections;

public class PlaylistObj {

	public int ID { get; set; }
	public string Name { get; set; }
	public string[] Files { get; set; }
	
	public PlaylistObj () {}

	public string ToString () {
		string output = ID + ", " + Name + ", ";
		foreach (string f in Files)
			output += f + ",";

		return output;
	}
}
