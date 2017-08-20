using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager> {

    public int fps = 60;

    private float frameInMs;
    private float gameStartTime;
    private int lastFrameCount;
    private List<FrameDependentEntity> entityList = new List<FrameDependentEntity>();

	// Use this for initialization
	void Start () {
        gameStartTime = Time.time;
        frameInMs = 0.001f * 60 / fps;
	}
	
	// Update is called once per frame
	void Update () {
        int currentFrameCount = (int)(Time.time / frameInMs);
        int framesSinceLastUpdate = currentFrameCount - lastFrameCount;

        for (int i = 0; i < framesSinceLastUpdate; i++)
        {
            foreach(FrameDependentEntity entity in entityList)
            {
                entity.UpdateFrame();
            }
        }

        lastFrameCount = currentFrameCount;
	}

    public void RegisterEntity(FrameDependentEntity entity)
    {
        entityList.Add(entity);
    }
}
