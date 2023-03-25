using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Native
{
    //永久解锁的迷雾信息
    private static int[] _serverFogInfo;
    private static int _bitCount = 8;
    private static int[, ] _fogPartInfo;
    //迷雾格数量
    private static int _mapFogCount = 410;
    private static int _fogPartUid = 0;
    private static Dictionary<int, List<int>> _fogLinkPartMap;

    private static Func<int, int, int[], bool> checkFunc;
    public static void InitFunc (Func<int, int, int[], bool> act)
    {
        checkFunc = act;
    }

    ///<summary>
    /// 第一次刷新迷雾(全量)
    ///</summary>
    public static void TestFunc1 (int[] fogInfo, int unlockCount)
    {
        Debug.LogError ("HybridCLR");
        _serverFogInfo = fogInfo;

        _fogPartUid = 0;
        _fogPartInfo = new int[_mapFogCount, _mapFogCount];
        _fogLinkPartMap = new Dictionary<int, List<int>> ();

        List<int> fogIds = new List<int> (unlockCount);

        for (int i = 0; i < _serverFogInfo.Length; i++)
        {
            for (int j = 0; j < _bitCount; j++)
            {
                int fogId = i * _bitCount + j;
                int row = fogId / _mapFogCount;
                int col = fogId - row * _mapFogCount;

                bool bUnlock = (_serverFogInfo[i] & (int)Math.Pow (2, j)) > 0;
                if (bUnlock)
                {
                    fogIds.Add (fogId);
                }
            }
        }

        foreach (var fogId in fogIds)
        {
            CheckFogLink (fogId);
        }
    }

    private static void CheckFogLink (int fogId)
    {
        int row = fogId / _mapFogCount;
        int col = fogId - row * _mapFogCount;

        if (_fogPartInfo[row, col] > 0)
        {
            return; //计算过了
        }

        List<int> partIds = new List<int> (4);
        for (int i = -1; i <= 1; i++)
        {
            int step = 2 - 2 * Mathf.Abs (i);
            step = step < 1 ? 1 : step;
            for (int j = Mathf.Abs (i) - 1; j <= Mathf.Abs (i) + 1; j += step)
            {
                int rowNew = i + row;
                int colNew = j + col;

                if (rowNew < 0 || rowNew >= _mapFogCount || colNew < 0 || colNew >= _mapFogCount)
                {
                    continue;
                }

                bool methodRes = checkFunc (rowNew, colNew, _serverFogInfo);
                if (!methodRes)
                    continue;

                int fogNewId = GetKeyByData (rowNew, colNew, _mapFogCount);
                int fogPartId = GetFogPartId (fogNewId);
                if (fogPartId > 0 && !partIds.Contains (fogPartId))
                {
                    partIds.Add (fogPartId);
                }
            }
        }

        if (partIds.Count < 1)
        {
            int tempPartId = GetUniqueFogPartId ();
            SetFogPartId (row, col, tempPartId);
            SetFogPartLinkMap (tempPartId, fogId);

            return;
        }

        partIds.Sort ((int a, int b) =>
        {
            return a <= b ? -1 : 1;
        });

        int targetFogPartId = partIds[0];
        partIds.RemoveAt (0);
        SetFogPartId (row, col, targetFogPartId);
        SetFogPartLinkMap (targetFogPartId, fogId);

        foreach (var scrId in partIds)
        {
            SetLinkRelation (targetFogPartId, scrId);
        }
    }

    ///<summary>
    /// 判断某个位置是否永久解锁
    ///</summary>
    private static bool CheckFogUnlockForever (int row, int col)
    {
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

    private static void SetLinkRelation (int targetId, int srcId)
    {
        if (!_fogLinkPartMap.ContainsKey (srcId))
        {
            return;
        }

        foreach (var cell in _fogLinkPartMap[srcId])
        {
            SetFogPartId (cell, targetId);
            SetFogPartLinkMap (targetId, cell);
        }

        _fogLinkPartMap.Remove (srcId);
    }

    private static void SetFogPartId (int row, int col, int partId)
    {
        _fogPartInfo[row, col] = partId;
    }

    private static void SetFogPartLinkMap (int partId, int fogId)
    {
        if (_fogLinkPartMap.ContainsKey (partId))
        {
            _fogLinkPartMap[partId].Add (fogId);

            return;
        }

        _fogLinkPartMap.Add (partId, new List<int> { fogId });
    }

    private static void SetFogPartId (int fogId, int partId)
    {
        int row = fogId / _mapFogCount;
        int col = fogId - row * _mapFogCount;

        _fogPartInfo[row, col] = partId;
    }

    private static int GetFogPartId (int fogId)
    {
        int row = fogId / _mapFogCount;
        int col = fogId - row * _mapFogCount;

        return _fogPartInfo[row, col];
    }

    private static int GetUniqueFogPartId ()
    {
        _fogPartUid++;
        return _fogPartUid;
    }
}