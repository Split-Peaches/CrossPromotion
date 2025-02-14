using System;
using System.Collections.Generic;

[Serializable]
public class GameInfo
{
    public string title;
    public string description;
    public string thumbnail;
    public string download;
}

[Serializable]
public class GameData
{
    public List<GameInfo> games;
}
