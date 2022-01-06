using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    public class TestCases 
    {
        //InvoiceGenerator Refrance
        InvoiceGenerator invoiceGenerator = null;
        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalFare()
        {
            //Creating Instance of InvoiceGenerator for Normal Ride
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;

            //Calculating Fare
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;

            //Asserting values
            Assert.AreEqual(expected, fare);
        }
    }
}