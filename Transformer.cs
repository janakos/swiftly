using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DataProcessor
{
    public class Transformer
    {
        // Used to store finalized data before turning into List<ProductRecord>
        Dictionary<string, List<object>> FinalizedData = new Dictionary<string, List<object>>();

        // Store data from FinalizedData into List<ProductRecord>
        public List<ProductRecord> TransformToProductRecord(Dictionary<string, List<string>> initialData)
        {
            // Apply all needed transformations to initialData: Dictionary<string, List<string>>
            TransformAllRows(initialData);

            // Create List<ProductRecords> for final output
            List<ProductRecord> records = new List<ProductRecord>();

            // Add data from FinalizedData: Dictionary<string, List<object>> into records: List<ProductRecord>
            for (int i = 0; i < this.FinalizedData["productId"].Count; i++)
            {
                ProductRecord pr = new ProductRecord();
                foreach (KeyValuePair<string, List<object>> item in this.FinalizedData)
                {
                    PropertyInfo propertyInfo = pr.GetType().GetProperty(item.Key);
                    propertyInfo.SetValue(pr, Convert.ChangeType(item.Value[i], propertyInfo.PropertyType), null);
                }
                records.Add(pr);
            }
            return records;
        }

        /*
         * This method lets us apply all needed transformations to all columsn in initialData: Dictionary<string, List<string>> simultaneously
         * We create a row Dictionary for both the current column being iterated on (currentRow: Dictionary<string, string>) 
         * and expected final output of that column (finalizedRow: Dictionary<string, object>)
         * 
         * We then apply all logic to transform currentRow into finalizedRow
         * Finally this finalizedRow is written back into FinalizedData in preparation to be written into List<ProductRecord>
         */
        private void TransformAllRows(Dictionary<string, List<string>> initialData)
        {
            InitializeFinalizedDataKeys();
            for (int i = 0; i < initialData["productId"].Count; i++)
            {
                Dictionary<string, string> currentRow = new Dictionary<string, string>();
                Dictionary<string, object> finalizedRow = new Dictionary<string, object>();
                foreach (string key in initialData.Keys)
                {
                    currentRow[key] = initialData[key][i];
                }

                // Run transformation per row
                finalizedRow = transformRow(currentRow, finalizedRow);

                // Write transformed data into FinalizedData: Dictionary<string, List<object>>
                foreach (PropertyInfo property in typeof(ProductRecord).GetProperties())
                {
                    this.FinalizedData[property.Name].Add(finalizedRow[property.Name]);
                }
            }
        }

        // Run transformation on currentRow to generate finalizedRow
        private Dictionary<string, object> transformRow(Dictionary<string, string> currentRow, Dictionary<string, object> finalizedRow)
        {
            finalizedRow["productId"] =                     currentRow["productId"];
            finalizedRow["productDescription"] =            currentRow["productDescription"];
            finalizedRow["regularDisplayPrice"] =           TransformDisplayPrice(currentRow, "regular");
            finalizedRow["regularCalculatorPrice"] =        TransformCalculatorPrice(currentRow, "regular");
            finalizedRow["promotionalDisplayPrice"] =       TransformDisplayPrice(currentRow, "promotional");
            finalizedRow["promotionalCalculatorPrice"] =    TransformCalculatorPrice(currentRow, "promotional");
            finalizedRow["unitOfMeasure"] =                 currentRow["flags"][2] == 'Y' ? "Pound" : "Each";
            finalizedRow["productSize"] =                   currentRow["productSize"].Trim();
            finalizedRow["taxRate"] =                       currentRow["flags"][4] == 'Y' ? 0.07775 : 0;
            return finalizedRow;
        }

        // Initialize keys of FinalizedData: Dictionary<string, List<object>>
        private void InitializeFinalizedDataKeys()
        {
            foreach (PropertyInfo property in typeof(ProductRecord).GetProperties())
            {
                this.FinalizedData[property.Name] = new List<object>();
            }
        }

        // Transformation methods

        private string TransformDisplayPrice(Dictionary<string, string> row, string priceLevel)
        {
            // Convert priceString to double, handle decimal appropriately
            double priceDouble = setPriceAsDouble(row, priceLevel);

            // Generate possible prefix if we are dealing with split price
            bool isSplit = Convert.ToDouble(row[priceLevel + "SplitPrice"]) != 0 ? true : false;
            string splitPrefix = isSplit ? Convert.ToInt16(row[priceLevel + "ForX"]).ToString() + " for " : "";

            return !Convert.ToBoolean(priceDouble) ? "0" : splitPrefix + "$" + priceDouble.ToString("F");
        }

        private decimal TransformCalculatorPrice(Dictionary<string, string> row, string priceLevel)
        {
            // Convert priceString to double, handle decimal appropriately
            decimal priceDouble = (decimal)setPriceAsDouble(row, priceLevel);

            // Calculate split denominator if we are dealing with split price
            int splitValue = Convert.ToInt16(row[priceLevel + "ForX"]);
            int splitDenominator = splitValue < 2 ? 1 : splitValue;

            return !Convert.ToBoolean(priceDouble) ? 0 : Math.Round(priceDouble / splitDenominator, 4, MidpointRounding.ToZero);
        }

        private double setPriceAsDouble(Dictionary<string, string> row, string priceLevel)
        {
            bool isSingular = Convert.ToDouble(row[priceLevel + "SingularPrice"]) != 0 ? true : false;
            string priceType = isSingular ? "Singular" : "Split";

            // Retrieve correct price value from input row
            string priceString = row[priceLevel + char.ToUpper(priceType[0]) + priceType.Substring(1) + "Price"];

            // Convert priceString to double, handle decimal appropriately
            return Convert.ToDouble(priceString.Substring(0, 6) + "." + priceString.Substring(6, 2));
        }
    }
}