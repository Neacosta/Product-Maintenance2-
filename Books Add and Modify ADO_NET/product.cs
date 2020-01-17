using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Add_and_Modify_ADO_NET
{
    public class product
    {
        //declaration of local attributes
        private string strCode;
        private string strDescription;
        private decimal decPrice;

        //constructor
        public product() { }

        //assign properties to passed in values with the product class.
        public product(string code, string description, decimal price)
        {
            this.Code = code;
            this.Description = description;
            this.Price = price;
        }

        // allow for properties to be set, by default set to local variables of blank.
        public string Code
        {
            get
            {
                return strCode;
            }
            set
            {
                strCode = value;
            }
        }

        public string Description
        {
            get
            {
                return strDescription;
            }
            set
            {
                strDescription = value;
            }
        }

        public decimal Price
        {
            get
            {
                return decPrice;
            }
            set
            {
                decPrice = value;
            }
        }


    }
}
