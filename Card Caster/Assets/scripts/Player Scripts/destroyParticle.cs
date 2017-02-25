using UnityEngine;
using System.Collections;

public class destroyParticle : MonoBehaviour {

    public float timeDelay;

	void Start () {
        Destroy(this.gameObject, timeDelay);
	}
}
