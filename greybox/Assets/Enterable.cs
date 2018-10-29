using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enterable {
	void OnEntered(GameObject other);
	void OnExited(GameObject other);
}
