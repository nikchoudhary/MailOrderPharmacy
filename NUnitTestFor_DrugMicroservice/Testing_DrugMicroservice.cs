using NUnit.Framework;
using MailOrderPharmacyDrugService.Repository;
using MailOrderPharmacyDrugService.Controllers;
using MailOrderPharmacyDrugService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System;

namespace NUnitTestFor_DrugMicroservice

{
    public class Testing_DrugMicroservice
    {

        // [SetUp]
        DrugDetails pcm = new DrugDetails()

        {
            DrugId = 1,
            Name = "Paracetamol",
            Manufacturer = "Jonsons",
            ManufacturedDate = new DateTime(2020, 09, 21),
            ExpiryDate = new DateTime(2021, 07, 12),
            cost = 100.00,
            UnitPackage = 100,
            Quantity = 100,
            drugLocation = new DrugLocation
            {
                Drug_Id = 1,
                Name = "Paracetamol",
                ExpiryDate = new DateTime(2022, 08, 10),
                Location = "Delhi",
                Available_Stock = 100
            }
        };

        List<DrugLocation> claimList = new List<DrugLocation>();
        [SetUp]
        public void Setup()
        {
            claimList = new List<DrugLocation>
            {
                new DrugLocation()
                {
                    Drug_Id = 1,
                    Name = "Paracetamol",
                    ExpiryDate = new DateTime(2022, 08, 10),
                    Location = "Delhi",
                    Available_Stock = 100
                }
            };
        }

        [Test]
        public void DrugByID_Valid_DrugDetails()
        {
            Mock<DrugRepository> mock = new Mock<DrugRepository>();
            mock.Setup(p => p.searchDrugsByID(1)).Returns(pcm);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.GetDrugDetails(1) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }
        [Test]
        public void DrugByID_InValid_DrugDetails()
        {
            Mock<DrugRepository> mock = new Mock<DrugRepository>();
            mock.Setup(p => p.searchDrugsByID(7)).Returns(pcm);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.GetDrugDetails(7) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        [Test]
        public void DrugByName_Valid_DrugDetails()
        {
            var mock = new Mock<DrugRepository>();
            mock.Setup(p => p.searchDrugsByName("Paracetamol")).Returns(pcm);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.GetDrugDetailByName("Paracetamol") as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }
        [Test]
        public void DrugByName_InValid_DrugDetails()
        {
            var mock = new Mock<DrugRepository>();
            mock.Setup(p => p.searchDrugsByName("Aciloc")).Returns(pcm);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.GetDrugDetailByName("Aciloc") as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        [Test]
        public void DrugByID_Location_Valid_DrugDetails()
        {
            var mock = new Mock<DrugRepository>();
            mock.Setup(p => p.GetDispatchableDrugStock(1, "Delhi")).Returns(claimList);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.getDispatchableDrugStock(1, "Delhi") as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }
        [Test]
        public void DrugByID_Location_InValid_DrugDetails()
        {
            var mock = new Mock<DrugRepository>();
            mock.Setup(p => p.GetDispatchableDrugStock(1, "Kolkata")).Returns(claimList);
            DrugsController con = new DrugsController(mock.Object);
            var data = con.getDispatchableDrugStock(10, "Kolkata") as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }



    }

}
