using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Invoice Generator class to generate the invoice
    /// </summary>
    public class InvoiceGenerator
    {
        //variables
        RideType rideType;
        private RideRepository rideRepository;

        //Constants
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        /// <summary>
        /// Constructor to Create Ride Repsitory Instance
        /// </summary>
        /// <param name="rideType"></param>
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                //if ridetype is Premium than Rates set for Premium else set for Normal
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoiceCustomException)
            {
                throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
            }
        }

        //Default Constructor
        public InvoiceGenerator()
        {

        }


        /// <summary>
        /// Function to Calculate Fare
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                //Calculating Total Fare
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoiceCustomException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.INVALID_RIDE_TYPE, "Invalid ride type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.INVALID_DISTANCE, "Invalid distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.INVALID_TIME, "Invalid time");

                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }

        ///<summary>
        ///Function To Calculate Cotal Fare and Generating Summary For Multiple Rides.
        /// </summary>
        /// <param name="rides"></param>
        /// <returns></returns>
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalfare = 0;
            try
            {
                //Calculating Total Fare For All Rides.
                foreach(Ride ride in rides)
                {
                    totalfare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceCustomException)
            {
                if(rides == null)
                {
                    throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalfare);
        }

        ///<summary>
        ///Function To Add Rides For UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                //Adding Ride To The Specified User.
                rideRepository.AddRide(userId, rides);
            }
            catch (CabInvoiceCustomException)
            {
                if(rides == null)
                {
                    throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
        }
        ///<summary>
        ///Function to Get Summary By UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch (CabInvoiceCustomException)
            {
                throw new CabInvoiceCustomException(CabInvoiceCustomException.ExceptionType.INVALID_USER_ID, "Invalid UserID");
            }
        }
    }
}