using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslater : MonoBehaviour
{
    public void MovePlayer() => PlayerTracker.SetPlayerPos(transform.position);
}
