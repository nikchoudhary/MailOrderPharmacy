using NUnit.Framework;
using MailOrderPharmacySubscription.Repository;
using MailOrderPharmacySubscription.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System;
using MailOrderPharmacySubscription.Modules;

namespace NUnitTest_SubscriptionMicroservice
{
    public class SubscriptionTest
    {

        // [SetUp]
        SubscriptionDetails db = new SubscriptionDetails()

        {

            Drug_ID = 2,
            Sub_id = 8,
            RefillOccurrence = "Monthly",
            Member_Location = "Pune"

        };



        [Test]
        public void ViewDetailsBySubscriptionID()
        {
            Mock<ISubscriptionRepository> mock = new Mock<ISubscriptionRepository>();
            mock.Setup(p => p.ViewDetailsByID(2)).Returns(db);
            SubscriptionController con = new SubscriptionController(mock.Object);
            var data = con.ViewDetails_BySubID(2) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        [Test]
        public void ViewDetailsBySubscriptionIDFail()
        {
            try
            {
                Mock<ISubscriptionRepository> mock = new Mock<ISubscriptionRepository>();
                mock.Setup(p => p.ViewDetailsByID(5)).Returns(null);

                SubscriptionController obj = new SubscriptionController(mock.Object);
                var res = obj.ViewDetails_BySubID(5) as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }

        [Test]
        public void AddSubscription_Valid_SubscriptionStatus()
        {
            Mock<ISubscriptionRepository> acr = new Mock<ISubscriptionRepository>();
            acr.Setup(p => p.AddSubscription(db, "Token")).Returns("Your Subscription Added Successfully!");
            SubscriptionController contr = new SubscriptionController(acr.Object);
            var data = contr.Add_Subscription(db) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        SubscriptionDetails sub = new SubscriptionDetails()

        {

            Drug_ID = 5,
            Sub_id = 8,
            RefillOccurrence = "Monthly",
            Member_Location = "Pune"

        };

        [Test]
        public void AddSubscription_InValid_SubscriptionStatus()
        {
           
                Mock<ISubscriptionRepository> mock = new Mock<ISubscriptionRepository>();
                mock.Setup(p => p.AddSubscription(sub, "Token")).Returns("Sorry! Subscription Not Possible Due To Unavailable drug.");

                SubscriptionController obj = new SubscriptionController(mock.Object);
                var res = obj.Add_Subscription(sub) as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
           
        }

        [Test]
        public void RemoveSubscription_Valid_SubscriptionStatus()
        {
            string str = "Unsubscription Done. Thank You!";
            Mock<ISubscriptionRepository> acr = new Mock<ISubscriptionRepository>();
            acr.Setup(p => p.RemoveSubscription(db, "Token")).Returns(str);
            SubscriptionController contr = new SubscriptionController(acr.Object);
            var data = contr.Remove_Subscription(db) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }


        [Test]
        public void RemoveSubscription_InValid_SubscriptionStatus()
        {
           
            string str = "Unsubscription Done. Thank You!";
            Mock<ISubscriptionRepository> acr = new Mock<ISubscriptionRepository>();
            acr.Setup(p => p.RemoveSubscription(sub, "Token")).Returns(str);
            SubscriptionController contr = new SubscriptionController(acr.Object);
            var data = contr.Remove_Subscription(sub) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }




    }

}
