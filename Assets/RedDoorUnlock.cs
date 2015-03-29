using UnityEngine;
using System.Collections;

public class RedDoorUnlock : DoorUnlock {



	protected override void open() {
		doorHinge.AddForce(-transform.forward * openingForce);
	}
}
