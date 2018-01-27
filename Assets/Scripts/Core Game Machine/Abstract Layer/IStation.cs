using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public interface IStation
{	
	/// <summary>
	/// 所屬地圖
	/// </summary>
	/// <value>The map.</value>
	IMap Map {
		get;
	}

	/// <summary>
	/// 主要 Transform
	/// </summary>
	Transform Transform {
		get;
	}

    /// <summary>
    /// 車站的索引
    /// </summary>
    int Index { get; }
    
    /// <summary>
    /// 判斷是否為主車站
    /// </summary>
	bool IsMainStation{get;}

    /// <summary>
    /// 依據帶入的索引判斷車站是否彼此臨近
    /// </summary>
	bool IsNeighbor(int index);

	/// <summary>
	/// 依據帶入判斷車站是否彼此臨近
	/// </summary>
	bool IsNeighbor(IStation station);

    /// <summary>
    /// 由系統生成乘客
    /// </summary>
    void NewPassenger(IPassenger passenger);

    /// <summary>
    /// 依據給定的 seats 數量，回傳可以上車的乘客數量
    /// </summary>
	void PickupPassengers(int seats, List<IPassenger> output);

}