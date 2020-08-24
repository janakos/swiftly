using System;
using DataProcessor;

namespace DataProcessor
{
    public class ProductRecord
    {
        public int productId { get; set; }
        public string productDescription { get; set; }
        public string regularDisplayPrice { get; set; }
        public double regularCalculatorPrice { get; set; }
        public string promotionalDisplayPrice { get; set; }
        public double promotionalCalculatorPrice { get; set; }
        public string unitOfMeasure { get; set; }
        public string productSize { get; set; }
        public double taxRate { get; set; }

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
                productId, productDescription,
                regularDisplayPrice, regularCalculatorPrice,
                promotionalDisplayPrice, promotionalCalculatorPrice,
                unitOfMeasure, productSize, taxRate
            );
        }
    }
}

