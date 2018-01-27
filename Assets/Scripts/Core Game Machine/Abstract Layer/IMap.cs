using System.Collections;
using System.Collections.Generic;

public interface IMap
{	

    /// <summary>
    /// 新增車站
    /// </summary>
	int AddStation(IStation station);

    /// <summary>
    /// 依據代入的索引取得車站
    /// </summary>
    IStation GetStation(int index);

	/// <summary>
	/// 依據代入的索引取得車站
	/// </summary>
	int GetStationIndex(IStation station);

	/// <summary>
	/// 新增車站連結
	/// </summary>
	/// <param name="stationA">Station a.</param>
	/// <param name="stationB">Station b.</param>
	void AddLink (IStation stationA, IStation stationB, IRoad road);

	/// <summary>
	/// 新增車站連結
	/// </summary>
	/// <param name="stationA">Station a.</param>
	/// <param name="stationB">Station b.</param>
	void AddLink (int indexA, int indexB, IRoad road);


	/// <summary>
	/// 取得前往鄰近車站的道路 (null) 表示非鄰近
	/// </summary>
	IRoad GetRoad (IStation stationA, IStation stationB);


	/// <summary>
	/// 依據帶入的索引判斷車站是否彼此臨近
	/// </summary>
	bool IsNeighbor(int indexA, int indexB);

	/// <summary>
	/// 依據帶入判斷車站是否彼此臨近
	/// </summary>
	bool IsNeighbor(IStation stationA, IStation stationB);

    /// <summary>
    /// 取得所有車站
    /// </summary>
	IEnumerable<IStation> GetAllStations();

	/// <summary>
	/// 取得所有車站
	/// </summary>
	void GetAllStations(List<IStation> output);

}