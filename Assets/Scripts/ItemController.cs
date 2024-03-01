using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	public int curState = Constants.STATE_OUT_CONVEYOR;

	public void ChangeState(int newState)
	{
		curState = newState;
	}
}
