using MailOrderPharmacyRefillService.Controllers;
using MailOrderPharmacyRefillService.Models;
using MailOrderPharmacyRefillService.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestRefill
{
    class RefillTest
    {
        List<RefillDetails> ls = new List<RefillDetails>()
        {
            new RefillDetails
            {
            RefillOrderId = 1,
            Subscription_ID = 7,
            DrugID = 1,
            RefillDate = new DateTime(2020, 09, 12),
            FromDate = new DateTime(2020, 05, 15),
            NextRefillDate = new DateTime(2020, 10, 08),
            Status = "pending",
            Policy_ID = 001,
            Member_ID = 01,
            Location = "Delhi"
            }
        };
        //[SetUp]
        RefillDetails cust = new RefillDetails()
        {
            RefillOrderId = 1,
            Subscription_ID = 7,
            DrugID = 1,
            RefillDate = new DateTime(2020, 09, 12),
            FromDate = new DateTime(2020, 05, 15),
            NextRefillDate = new DateTime(2020, 10, 08),
            Status = "pending",
            Policy_ID = 001,
            Member_ID = 01,
            Location = "Delhi"
        };

        RefillOrderLine ol = new RefillOrderLine()
        {
            Policy_ID = 001,
            Member_ID = 01,
            Subscription_ID = 7,
            Location = "Delhi"
            
         };
        RefillOrderLine rl = new RefillOrderLine()
        {
            Policy_ID = 001,
            Member_ID = 02,
            Subscription_ID = 6,
            Location = "Pune"

        };


        [Test]
    public void RefillById_Valid_RefillDetails()
    {
        Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
        mock.Setup(p => p.viewRefillStatus(7)).Returns(cust);
        RefillController r = new RefillController(mock.Object);
        var data = r.RefillStatus(7) as OkObjectResult;
        Assert.AreEqual(200, data.StatusCode);
    }

    [Test]
    public void RefillById_InValid_RefillDetails()
    {
        try
        {
            RefillDetails obj = new RefillDetails();
            Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
            mock.Setup(p => p.viewRefillStatus(10)).Returns(null);
            RefillController r = new RefillController(mock.Object);
            var data = r.RefillStatus(10) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);
        }
        catch (Exception e)
        {
            Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
        }
     }
        
        [Test]
        public void RefillDuesDate_Valid_NextDate()
        {   
                Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
                mock.Setup(p => p.PendingRefill(7, new DateTime(2020, 06, 12), "Token")).Returns(ls);

                RefillController obj = new RefillController(mock.Object);
                var res = obj.RefillDueAsOfDate(7, "2020, 06, 12") as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
                


        }

        [Test]
        public void RefillDuesDate_InValid_NextDate()
        {
                Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
                mock.Setup(p => p.PendingRefill(5, new DateTime(2020, 06, 12), "Token")).Returns(ls);

                RefillController obj = new RefillController(mock.Object);
                var res = obj.RefillDueAsOfDate(5, "2020, 06, 12") as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            

        }
        [Test]
        public void RequestAdhocRefill_Valid_RefillDetails()
        {
               Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
                mock.Setup(p => p.requestAdhocRefill(ol, "Token")).Returns(cust);

                RefillController obj = new RefillController(mock.Object);
                var res = obj.requestAdhocRefill(ol) as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void RequestAdhocRefill_InValid_RefillDetails()
        {
            try
            {
                Mock<IRefillRepository> mock = new Mock<IRefillRepository>();
                mock.Setup(p => p.requestAdhocRefill(rl, "Token")).Returns(null);

                RefillController obj = new RefillController(mock.Object);
                var res = obj.requestAdhocRefill(rl) as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

    }
    }

