using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[TestFixture] 
public class UnitTest
{
    [Test]
    public void PassengerArriveTest() {
        IStation s1 = new Station();
        IStation s2 = new Station();
        IStation s3 = new Station(); 
        IPassenger p = new Passenger(s1, s2, null);
        
        Assert.AreEqual(true, p.IsWaiting);

        int distance = 3;
        int capacity = 5;
        IBus b = new Bus(distance, capacity);

        List<IStation> path = new List<IStation>() {
            s1, s2, s3
        };
        b.StartBusPath(path);
        b.PassThroughStation(s1);

        Assert.AreEqual(true, p.IsMoving);

        b.PassThroughStation(s2);

        Assert.AreEqual(true, p.IsArrived);

        b.PassThroughStation(s3);
    }

    [Test]
    public void PassengerGetOffTest() {
        IStation s1 = new Station();
        IStation s2 = new Station();
        IStation s3 = new Station();
        IStation s4 = new Station();
        IPassenger p = new Passenger(s1, s4, null);
        
        Assert.AreEqual(true, p.IsWaiting);

        int distance = 3;
        int capacity = 5;
        IBus b = new Bus(distance, capacity);

        List<IStation> path = new List<IStation>() {
            s1, s2, s3
        };
        b.StartBusPath(path);
        b.PassThroughStation(s1);

        Assert.AreEqual(true, p.IsMoving);

        b.PassThroughStation(s2);

        Assert.AreEqual(true, p.IsMoving);

        b.PassThroughStation(s3);

        Assert.AreEqual(true, p.IsWaiting);
    }

    [Test]
    public void PassengerTooManyTest() {
        IStation s1 = new Station();
        IStation s2 = new Station();
        IStation s3 = new Station();
        IPassenger p1 = new Passenger(s1, s3, null);
        IPassenger p2 = new Passenger(s1, s3, null);
        IPassenger p3 = new Passenger(s2, s3, null);
        IPassenger p4 = new Passenger(s2, s3, null);

        Assert.AreEqual(true, p1.IsWaiting);
        Assert.AreEqual(true, p2.IsWaiting);
        Assert.AreEqual(true, p3.IsWaiting);
        Assert.AreEqual(true, p4.IsWaiting);

        int distance = 3;
        int capacity = 3;
        IBus b = new Bus(distance, capacity);

        List<IStation> path = new List<IStation>() {
            s1, s2, s3
        };
        b.StartBusPath(path);
        b.PassThroughStation(s1);

        Assert.AreEqual(true, p1.IsMoving);
        Assert.AreEqual(true, p2.IsMoving);
        Assert.AreEqual(true, p3.IsWaiting);
        Assert.AreEqual(true, p4.IsWaiting);

        b.PassThroughStation(s2);

        Assert.AreEqual(true, p1.IsMoving);
        Assert.AreEqual(true, p2.IsMoving);
        Assert.AreEqual(true, p3.IsMoving);
        Assert.AreEqual(true, p4.IsWaiting);

        b.PassThroughStation(s3);

        Assert.AreEqual(true, p1.IsArrived);
        Assert.AreEqual(true, p2.IsArrived);
        Assert.AreEqual(true, p3.IsArrived);
        Assert.AreEqual(true, p4.IsWaiting);
    }


    [Test]
    public void PassThroughStatusTest() {
        IStation s1 = new Station();
        IStation s2 = new Station();
        IStation s3 = new Station();
        IStation s4 = new Station();
        IPassenger p1 = new Passenger(s1, s3, null);
        IPassenger p2 = new Passenger(s1, s4, null);
        IPassenger p3 = new Passenger(s2, s3, null);
        IPassenger p4 = new Passenger(s2, s3, null);

        Assert.AreEqual(true, p1.IsWaiting);
        Assert.AreEqual(true, p2.IsWaiting);
        Assert.AreEqual(true, p3.IsWaiting);
        Assert.AreEqual(true, p4.IsWaiting);

        int distance = 3;
        int capacity = 3;
        IBus b = new Bus(distance, capacity);

        List<IStation> path = new List<IStation>() {
            s1, s2, s3
        };
        b.StartBusPath(path);
        List<IPassenger> pass1 = b.PassThroughStation(s1);

        Assert.AreEqual(2, pass1.Count);
        Assert.AreEqual(true, pass1[0].IsMoving);
        Assert.AreEqual(true, pass1[1].IsMoving);

        List<IPassenger> pass2 = b.PassThroughStation(s2);

        Assert.AreEqual(1, pass2.Count);
        Assert.AreEqual(true, pass2[0].IsMoving);

        List<IPassenger> pass3 = b.PassThroughStation(s3);

        Assert.AreEqual(3, pass3.Count);
        Assert.AreEqual(true, pass3[0].IsArrived);
        Assert.AreEqual(true, pass3[1].IsWaiting);
        Assert.AreEqual(true, pass3[2].IsArrived);
    }
}