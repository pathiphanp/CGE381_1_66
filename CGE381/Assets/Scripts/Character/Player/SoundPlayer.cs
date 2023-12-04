using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
   void SoundWalkAndRun1()
   {
      SoundManager.Instance.PlaySfx("PlayerWalkAndRun1");
   }
   void SoundWalkAndRun2()
   {
      SoundManager.Instance.PlaySfx("PlayerWalkAndRun2");
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

   void SoundWaterDie()
   {
      SoundManager.Instance.PlaySfx("WaterDie");
   }
}
