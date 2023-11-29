using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
   void SoundWalkAndRun()
   {
      SoundManager.Instance.PlaySfx("WalkAndRun");
   }
   void SoundFastDown()
   {
      SoundManager.Instance.PlaySfx("Downfast");
   }
   void SoundNormalDown()
   {
      SoundManager.Instance.PlaySfx("DownNormal");
   }
   void SoundSlowDown()
   {
      SoundManager.Instance.PlaySfx("DownSlow");
   }
}
