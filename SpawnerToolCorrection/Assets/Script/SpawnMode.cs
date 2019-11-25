using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnMode
{
    public SpawnType Type = SpawnType.Circle;

    public CircleMode CircleMode = new CircleMode();
    public LineMode LineMode = new LineMode();
    public PointMode PointMode = new PointMode();

    public Mode Mode
    {
        get
        {
            switch (Type)
            {
                case SpawnType.Circle:
                    return CircleMode;
                case SpawnType.Line:
                    return LineMode;
                case SpawnType.Point:
                    return PointMode;
                default:
                    return null;
            }
        }
    }
}


public enum SpawnType
{

    Circle,
    Line,
    Point

}