// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pada1.BBCore;
// using Pada1.BBCore.Framework;
// using BBUnity.Actions;
// [Action("Game/FollowEnemy")]
// public class FollowEnemy : BasePrimitiveAction {
//     // Start is called before the first frame update
//     [InParam("spawned")]
//     public GameObject spawned;
//     public override void OnStart()
//     {
//         base.OnStart();
//         Around component = this.spawned.GetComponent<Around>();
//         if(component == null) return;
//         Debug.Log(component);
//         component.StartCoroutine(component.MoveToRandomPosition());
//     }
// }

