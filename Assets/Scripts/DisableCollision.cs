using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollision : FallScript {
    public override void EnableFalling(){
      var colliders = this.gameObject.GetComponents<Collider>();
      foreach (var col in colliders)
      {
          col.enabled = false;
      }
    }
}
