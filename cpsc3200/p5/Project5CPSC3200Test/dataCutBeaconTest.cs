﻿using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project5CPSC3200;
using System;

namespace Project5CPSC3200Test {
    [TestClass]
    public class dataCutBeaconTest {
        [TestMethod]
        public void Test_dataCutBeacon_SignalSetSeq() {
            // Arrange
            dataCutBeacon bc = new dataCutBeacon();
            int[] testArray = new int[] { 1, 2, 3, 4, 5 };
            int[] signalArray = new int[5];
            int[] assertArray = new int[] { 1, 2, 3, 4, 5 };

            // Act
            bc.setSeq(testArray);
            for (int i = 0; i < signalArray.Length; i++) {
                signalArray[i] = bc.signal();
            }

            // Assert
            CollectionAssert.AreEqual(signalArray, assertArray);
        }
        [TestMethod]
        public void Test_dataCutBeacon_ChargeFns() {
            // Arrange
            dataCutBeacon bc = new dataCutBeacon();
            int[] testArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] signalArray = new int[11];
            int[] assertArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0 };

            // Act
            bc.setSeq(testArray);
            for (int i = 0; i < signalArray.Length; i++) {
                signalArray[i] = bc.signal();
            }
            bc.recharge(5);

            // Assert
            Assert.AreEqual(bc.getCharge(), 5);
            CollectionAssert.AreEqual(signalArray, assertArray);
        }

        [TestMethod]
        public void Test_dataCutBeacon_StateFns() {
            // Arrange
            dataCutBeacon bc = new dataCutBeacon();
            int[] testArray = new int[] { 1, 2, 3, 4, 5 };
            int[] signalArray = new int[5];
            int[] assertArray = new int[] { 1, 0, 2, 0, 3 };

            // Act
            bc.setSeq(testArray);
            for (int i = 0; i < signalArray.Length; i++) {
                if (i % 2 == 0) {
                    bc.turnOn();
                } else {
                    bc.turnOff();
                }
                signalArray[i] = bc.signal();
            }

            // Assert
            CollectionAssert.AreEqual(signalArray, assertArray);
        }
        [TestMethod]
        public void Test_dataCutBeacon_FilterSmallMode() {
            // Arrange
            dataCutBeacon d = new dataCutBeacon(17);
            int[] testArray = new int[6] { 23, 28, 4, 20, 5, 58 };
            int[] assertArray = new int[1] { 5 };

            // Act
            d.scramble(testArray);
            int[] dFilter = d.filter();

            // Assert
            CollectionAssert.AreEqual(dFilter, assertArray);
        }

        [TestMethod]
        public void Test_dataCutBeacon_FilterLargeMode() {
            // Arrange
            dataCutBeacon d = new dataCutBeacon(19);
            d.setMode(true);
            int[] testArray = new int[6] { 23, 28, 4, 20, 5, 58 };
            int[] assertArray = new int[3] { 23, 28, 20 };

            // Act
            d.scramble(testArray);
            int[] dFilter = d.filter();

            // Assert
            CollectionAssert.AreEqual(dFilter, assertArray);
        }

        [TestMethod]
        public void Test_dataCutBeacon_ScrambleRemoveLastScrambled() {
            // Arrange
            dataCutBeacon d = new dataCutBeacon(23);
            int[] testArray1 = new int[4] { 24, 17, 4, 10 }; // 17, 24, 4, 10
            int[] testArray2 = new int[] { 29, 14, 4, 33, 7 };
            int[] assertArray = new int[] { 14, 29, 7, 33 };

            // Act
            d.scramble(testArray1);
            int[] dScramble = d.scramble(testArray2);

            // Assert
            CollectionAssert.AreEqual(dScramble, assertArray);
        }
    }
}
