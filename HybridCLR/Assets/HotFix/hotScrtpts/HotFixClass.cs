using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HotFixClass
{
    private static int _bitCount = 8;
    //迷雾格数量
    private static int _mapFogCount = 410;
    ///<summary>
    /// 判断某个位置是否永久解锁
    ///</summary>
    public static bool HotFixFunc (int row, int col, int[] _serverFogInfo)
    {
        // Debug.LogError("wolong");
        int fogIndex = GetKeyByData (row, col, _mapFogCount);
        int fogAreaId = fogIndex / _bitCount;
        int fogAreaIndex = fogIndex - fogAreaId * _bitCount;

        if (_serverFogInfo == null || fogAreaId < 0 || _serverFogInfo.Length <= fogAreaId)
        {
            //Debug.LogError("迷雾数据越界 row = " + row + "  col = " + col);
            return false;
        }

        return (_serverFogInfo[fogAreaId] & (int)Math.Pow (2, fogAreaIndex)) > 0;
    }

    private static int GetKeyByData (int row, int col, int count)
    {
        int key = count * row + col;
        return key;
    }
}