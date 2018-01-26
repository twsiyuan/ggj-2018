using System.Collections.Generic;
public interface IStation
{	

    /// <summary>
    /// 車站的索引
    /// </summary>
    int Index { get; }
    
    /// <summary>
    /// 判斷是否為主車站
    /// </summary>
    bool IsMainStation(); 

    /// <summary>
    /// 依據帶入的索引判斷車站是否彼此臨近
    /// </summary>
	bool IsNeighbor(int index);


    /// <summary>
    /// 由系統生成乘客
    /// </summary>
    void NewPassenger(IPassenger passenger);

    /// <summary>
    /// 依據給定的 seats 數量，回傳可以上車的乘客數量
    /// </summary>
    List<IPassenger> GetPassengers(int seats);

}