using System;
using DataProcessor;

namespace DataProcessor
{
    public class ProductRecord
    {
        public int product_id { get; set; } // can we cast object during set method??
        public string product_description { get; set; }
        public string regular_display_price { get; set; }
        public double regular_calculator_price { get; set; }
        public string promotional_display_price { get; set; }
        public double promotional_calculator_price { get; set; }
        public string unit_of_measure { get; set; }
        public string product_size { get; set; }
        public double tax_rate { get; set; }

        public override string ToString()
        {
            return String.Format(
                "product_id: {0}\n" +
                "product_description: {1}\n" +
                "regular_display_price: {2}\n" +
                "regular_calculator_price: {3}\n" +
                "promotional_display_price: {4}\n" +
                "promotional_calculator_price: {5}\n" +
                "unit_of_measure: {6}\n" +
                "product_size: {7}\n" +
                "tax_rate: {8}\n",
                product_id, product_description,
                regular_display_price, regular_calculator_price,
                promotional_display_price, promotional_calculator_price,
                unit_of_measure, product_size, tax_rate
            );
        }
    }
}

