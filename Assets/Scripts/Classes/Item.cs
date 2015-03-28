using UnityEngine;
using System.Collections;

public enum Item {

	KEY_DOLLHOUSE_DOOR = 1 << 1,
	KEY_INTRO_DOOR = 1 << 2,
	ANYTHING_NOTHING = 0,
	EVERYTHING = KEY_DOLLHOUSE_DOOR | KEY_INTRO_DOOR

}
