using UnityEngine;
using System.Collections;

public class Trace {
	public bool vertical;
	public int index, start, end;

	public Trace()
	{
		vertical = false;
		index = -1;
		start = end = 0;
		Debug.Log ("TraceCon");
	}

	public void setTrace ( Vector2 vStart, Vector2 vDestination )
	{
		int start_x = (int)vStart.x;
		int start_y = (int)vStart.y;
		int dest_x = (int)vDestination.x;
		int dest_y = (int)vDestination.y;


		if ( start_x == dest_x )
		{
			vertical = false;
			index = start_x;
			if ( start_y < dest_y )
			{
				start = start_y;
				end = dest_y;
			}
			else
			{
				start = dest_y;
				end = start_y;
			}
		}
		else
		{
			vertical = true;
			index = start_y;
			if ( start_x < dest_x )
			{
				start = start_x;
				end = dest_x;
			}
			else
			{
				start = dest_x;
				end = start_x;
			}
		}
	}
}

