using System.Collections;
using System.Collections.Generic;

public interface IMap
{	

    /// <summary>
    /// 是否還有隱藏的車站
    /// </summary>
    bool HasHidingStation();

    /// <summary>
    /// 新增車站
    /// </summary>
    void AddStation();

    /// <summary>
    /// 依據代入的索引取得車站
    /// </summary>
    IStation GetStation(int index);

    /// <summary>
    /// 取得所有車站
    /// </summary>
    List<IStation> GetAllStations();

}