using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceSummary
    {
        //Varables.
        private int numberOfRides;
        private double totalFare;
        private double averageFare;

        ///<summary>
        ///Parameter Constructor For Setting Data.
        /// </summary>
        /// <param name="numberOfRides"></param>
        /// <param name="totalfare"></param>
        
        public InvoiceSummary(int numberOfRides, double totalFare)
        {
            //Setting Data,
            this.numberOfRides = numberOfRides;
            this.totalFare = totalFare;
            this.averageFare = totalFare / numberOfRides;
        }

        ///<summary>
        ///Overriding Equals Method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public override bool Equals(object obj)
        {
           if(obj == null) return false;
           if(!(obj is InvoiceSummary)) return false;
           InvoiceSummary inputedobject = (InvoiceSummary)obj;
            return this.numberOfRides == inputedobject.numberOfRides && this.totalFare == inputedobject.totalFare && this.averageFare == inputedobject.averageFare;
        }

        ///<summary>
        ///Overriding GetHashCode Method.
        /// </summary>
        /// <returns></returns>

        public override int GetHashCode()
        {
            return this.numberOfRides.GetHashCode() ^ this.totalFare.GetHashCode() ^ this.averageFare.GetHashCode();
        }
    }
}
