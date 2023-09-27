using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class SpawnCutScenes : MonoBehaviour
{
    public static event Action EndCutScene;
    [SerializeField] public GameObject[] cutScenes;
    [SerializeField] int indexCutScene = 0;
    public bool canSpawn;
    public ControlCutScenes controlCutScenes;
    [SerializeField] bool CutScenes;
    [SerializeField] bool CameraScenes;
    [SerializeField] PlayableDirector director;
    private void Start()
    {
        if (CutScenes)
        {
            SpawnCutScene();
        }
        else if (CameraScenes)
        {
            CameraCutScenes();
        }
    }

    private void CameraCutScenes()
    {
        director.Play();
    }

    public void SpawnCutScene()
    {
        if (indexCutScene == cutScenes.Length)
        {
            SpawnCutScenes.EndCutScene();
        }
        else
        {
            if (canSpawn)
            {
                canSpawn = false;
                if (SpawnCutScenes.EndCutScene != null)
                {
                    GameObject c = Instantiate(cutScenes[indexCutScene], transform.position, transform.rotation, transform.parent);
                    controlCutScenes = c.GetComponent<ControlCutScenes>();
                }
                controlCutScenes.spawnCutScenes = this;
            }
        }
        indexCutScene++;
    }

    public static void EndCutSceneEvent()
    {
        EndCutScene?.Invoke();
    }
}
