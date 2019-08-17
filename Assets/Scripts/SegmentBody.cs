using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBody : Part {

    public static ArrayList centipede;
    public int amountSegments = 0;
    public static int segmentNumber = 0;

    public SegmentBody()
    {
        centipede = new ArrayList();
    }

    public static int AddSegment()
    {
        return segmentNumber++;
    }
}
