using System;
using Godot;
using Godot.Collections;

public partial class MapGenerator : Node2D
{
    private TileMap tileMap;

    private Array<Dictionary> mapList;

    [Export]
    private Array<int> mapRandoms;

    [Export]
    private int seed;

    [Export]
    private int mapHeight, mapWidth;

    [Export]
    private int cityCount, cityMaxHeight, cityMaxWidth, cityMinHeight, cityMinWidth, urbanSpacing;

    [Export]
    private int houseCount, houseMaxHeight, houseMaxWidth, houseMinHeight, houseMinWidth, houseMinLength, houseMaxLength;

    [Export]
    private int houseRoomMaxCount, houseRoomMinCount;

    public override void _Ready()
    {
        mapRandoms.Add(seed);
        Init();
    }

    int ApplyRandom()
    {
        if (mapRandoms.Count != 0)
        {
            mapRandoms.Add(new Random(mapRandoms[mapRandoms.Count]).Next());
        }
        return mapRandoms[mapRandoms.Count];
    }


    // 初始化地图列表以及城市等
    void Init()
    {
        int count = 0;
        while (count < cityCount)
        {
            mapList.Add(InitCity());
            count++;
        }
    }

    Dictionary InitCity()
    {
        // 0: 上 1: 下 2: 左 3: 右
        Godot.Collections.Array<Dictionary> houses = new Godot.Collections.Array<Dictionary>();
        int length = new Random(ApplyRandom()).Next(houseMinLength, houseMaxLength);
        int nowHouseCount = 0;
        Vector2 pos = Vector2.Zero;

        // 第一个房屋的生成
        if (nowHouseCount == 0)
        {
            houses.Add(InitHouse(pos));
            nowHouseCount++;
        }

        while (houseCount > nowHouseCount)
        {
            // 房屋的生成方向
            //int houseDirection = new Random(ApplyRandom()).Next(0,4);
            bool isBalanceHouse = false;
            // 四周是否有房屋
            // 如果左上方有房屋则在右边生成房屋并且在右上方也生成房屋
            if (houses[nowHouseCount].ContainsKey(pos + Vector2.Up))
            {
                
            }
            else if (houses[nowHouseCount].ContainsKey(pos + Vector2.Down))
            {
                
            }
            else if (houses[nowHouseCount].ContainsKey(pos + Vector2.Left))
            {
                
            }
            else if (houses[nowHouseCount].ContainsKey(pos + Vector2.Right))
            {
                
            }
            nowHouseCount++;
        }
        Dictionary cityData = new Dictionary
        {
        };
        return cityData;
    }

    Dictionary InitHouse(Vector2 pos)
    {
        int roomCount = new Random(ApplyRandom()).Next(houseRoomMinCount, houseRoomMaxCount);
        int nowRoomCount = 0;
        Dictionary houseData = new Dictionary
        {
            { pos, pos },
            { "rooms", new Godot.Collections.Array<Dictionary>() }
        };
        while (nowRoomCount < roomCount)
        {
            nowRoomCount++;
        }
        return houseData;
    }

    Dictionary InitHouseRoom()
    {
        Dictionary roomData = new Dictionary
        {

        };
        return roomData;
    }

    void Setup()
    {
    }
}
